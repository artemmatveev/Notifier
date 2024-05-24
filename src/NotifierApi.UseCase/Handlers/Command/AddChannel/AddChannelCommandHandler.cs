namespace NotifierApi.UseCase.Handlers.Command.AddChannel
{
    internal sealed class AddChannelCommandHandler : IRequestHandler<AddChannelCommand, Channel>
    {
        readonly IChannelRepository _channelRepository;
        public AddChannelCommandHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<Channel> Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = Channel.Create(request.Name, request.Data, request.Transport);

            return await _channelRepository.AddAsync(channel, e => e.Name != channel.Name);
        }
    }
}
