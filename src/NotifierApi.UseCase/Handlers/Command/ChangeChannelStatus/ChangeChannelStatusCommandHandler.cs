namespace NotifierApi.UseCase.Handlers.Command.ChangeChannelStatus
{
    sealed class ChangeChannelStatusCommandHandler : IRequestHandler<ChangeChannelStatusCommand, Unit>
    {
        readonly IChannelRepository _channelRepository;
        public ChangeChannelStatusCommandHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<Unit> Handle(ChangeChannelStatusCommand request, CancellationToken cancellationToken)
        {
            await _channelRepository.UpdateAsync(request.Id, e => e.ChangeStatus(request.Status));

            return Unit.Value;
        }
    }
}
