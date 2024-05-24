namespace NotifierApi.UseCase.Handlers.Query.FindTemplates
{
    internal sealed class FindTemplatesQueryHandler : IRequestHandler<FindTemplatesQuery, IReadOnlyList<Template>>
    {
        readonly ITemplateRepository _templateRepository;

        public FindTemplatesQueryHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<IReadOnlyList<Template>> Handle(FindTemplatesQuery query, CancellationToken cancellationToken)
            => await _templateRepository.FindAllAsync(query.GetExpression(), nameof(Notification.Name), SortOrder.Asc);
    }
}
