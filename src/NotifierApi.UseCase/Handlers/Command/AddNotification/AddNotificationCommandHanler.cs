using Iems.Framework.Core.Exceptions;

namespace NotifierApi.UseCase.Handlers.Command.AddNotification
{
    internal sealed class AddNotificationCommandHanler : IRequestHandler<AddNotificationCommand, Notification>
    {
        readonly INotificationRepository _notificationRepository;
        readonly IChannelRepository _channelRepository;
        readonly IApplicationRepository _applicationRepository;

        public AddNotificationCommandHanler(INotificationRepository notificationRepository,
            IChannelRepository channelRepository, 
            IApplicationRepository applicationRepository)
        {
            _notificationRepository = notificationRepository;
            _channelRepository = channelRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<Notification> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var channels = await _channelRepository.FindAllAsync(e => request.ChannelIds.Contains(e.Id));
            if (channels.Count != request.ChannelIds.Count)
                throw new BusinessRuleException("Any Channel ids don't find");

            var application = await _applicationRepository.FindAsync(e => e.Id == request.ApplicationId);
            if (application is null)
                throw new BusinessRuleException("Application don't find");

            var notification = Notification
                .Create(application, request.Priority, request.Name, request.Comment, channels);

            return await _notificationRepository.AddAsync(notification, e => e.Name != notification.Name);
        }
    }
}
