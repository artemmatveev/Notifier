namespace NotifierApi.UseCase.Handlers.Query.FindApplications
{
    internal sealed class FindApplicationsQueryHandler : IRequestHandler<FindApplicationsQuery, IReadOnlyList<Application>>
    {
        readonly IApplicationRepository _applicationRepository;
        public FindApplicationsQueryHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IReadOnlyList<Application>> Handle(FindApplicationsQuery query, CancellationToken cancellationToken)
            => await _applicationRepository.FindAllAsync(query.GetExpression(), nameof(Application.Name), SortOrder.Asc);
    }
}
