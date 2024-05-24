namespace NotifierApi.UseCase.Handlers.Query.FindChannels
{
    internal sealed class FindChannelsQueryHandler : IRequestHandler<FindChannelsQuery, IReadOnlyList<Channel>>
    {
        readonly IChannelRepository _channelRepository;
        public FindChannelsQueryHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<IReadOnlyList<Channel>> Handle(FindChannelsQuery query, CancellationToken cancellationToken)
            => await _channelRepository.FindAllAsync(query.GetExpression(), nameof(Channel.Name), SortOrder.Asc);

    }
}
