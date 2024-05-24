using Iems.Framework.Core.Exceptions;

namespace NotifierApi.UseCase.Handlers.Command.UpdateNotification
{
    internal sealed class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, Unit>
    {
        readonly INotificationRepository _notificationRepository;
        readonly IChannelRepository _channelRepository;        

        public UpdateNotificationCommandHandler(INotificationRepository notificationRepository,
            IChannelRepository channelRepository)
        {
            _notificationRepository = notificationRepository;
            _channelRepository = channelRepository;
        }

        public async Task<Unit> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var channels = await _channelRepository.FindAllAsync(e => request.ChannelIds.Contains(e.Id));
            if (channels.Count != request.ChannelIds.Count)
                throw new BusinessRuleException("Any Channel ids don't find");

            var notification = await _notificationRepository
                .GetWithChannelsAsync(ch => ch.Id == request.Id, true);

            notification.Update(request.Priority, request.Name, request.Comment, channels);
            await _notificationRepository.UpdateAsync(notification);

            return Unit.Value;
        }
    }
}
