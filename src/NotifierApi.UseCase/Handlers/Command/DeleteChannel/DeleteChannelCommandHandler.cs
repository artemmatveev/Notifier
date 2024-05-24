namespace NotifierApi.UseCase.Handlers.Command.DeleteChannel
{
    internal sealed class DeleteChannelCommandHandler : IRequestHandler<DeleteChannelCommand, Unit>
    {
        readonly IChannelRepository _channelRepository;
        public DeleteChannelCommandHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<Unit> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
        {
            await _channelRepository.UpdateAsync(request.Id, e => e.Delete());

            return Unit.Value;
        }
    }
}
