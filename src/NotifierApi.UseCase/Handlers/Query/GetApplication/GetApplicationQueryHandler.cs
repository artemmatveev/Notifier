namespace NotifierApi.UseCase.Handlers.Query.GetApplication
{
    internal sealed class GetApplicationQueryHandler : IRequestHandler<GetApplicationQuery, Application>
    {
        readonly IApplicationRepository _applicationRepository;

        public GetApplicationQueryHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> Handle(GetApplicationQuery query, CancellationToken cancellationToken)
            => await _applicationRepository.GetAsync(e => e.Id == query.Id);
    }
}
