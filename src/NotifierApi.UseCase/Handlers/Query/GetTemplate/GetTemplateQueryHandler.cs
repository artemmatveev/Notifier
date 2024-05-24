namespace NotifierApi.UseCase.Handlers.Query.GetTemplate
{
    internal sealed class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, Template>
    {
        ITemplateRepository _templateRepository;

        public GetTemplateQueryHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Template> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
            => await _templateRepository.GetAsync(e => e.Id == request.Id);
    }
}
