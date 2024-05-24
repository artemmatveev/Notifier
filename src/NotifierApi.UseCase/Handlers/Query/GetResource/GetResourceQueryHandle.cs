namespace NotifierApi.UseCase.Handlers.Query.GetResource
{
    internal sealed class GetResourceQueryHandler : IRequestHandler<GetResourceQuery, Resource>
    {
        readonly IResourceRepository _resourceRepository;

        public GetResourceQueryHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Resource> Handle(GetResourceQuery request, CancellationToken cancellationToken)
            => await _resourceRepository.GetAsync(e => e.Id == request.Id);
    }
}
