namespace NotifierApi.UseCase.Handlers.Query.FindResources
{
    internal sealed class FindResourcesQueryHandler : IRequestHandler<FindResourcesQuery, IReadOnlyList<Resource>>
    {
        readonly IResourceRepository _resourceRepository;
        public FindResourcesQueryHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<IReadOnlyList<Resource>> Handle(FindResourcesQuery query, CancellationToken cancellationToken)
            => await _resourceRepository.FindAllAsync(query.GetExpression(), nameof(Resource.Name), SortOrder.Asc);
    }
}
