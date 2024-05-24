namespace NotifierApi.UseCase.Handlers.Command.UpdateChannel
{
    internal sealed class UpdateChannelCommandHandler : IRequestHandler<UpdateChannelCommand, Unit>
    {
        readonly IChannelRepository _channelRepository;
        public UpdateChannelCommandHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<Unit> Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
        {
            await _channelRepository.UpdateAsync(request.Id,

                e => e.Update(request.Name, request.Data, request.Transport),

                e => e.Name != request.Name || (e.Name == request.Name && e.Id == request.Id));

            return Unit.Value;
        }
    }
}
