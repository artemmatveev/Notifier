namespace NotifierApi.UseCase.Handlers.Query.GetChannel
{
    internal sealed class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, Channel>
    {
        readonly IChannelRepository _channelRepository;

        public GetChannelQueryHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<Channel> Handle(GetChannelQuery query, CancellationToken cancellationToken)
            => await _channelRepository.GetAsync(e => e.Id == query.Id);

    }
}
