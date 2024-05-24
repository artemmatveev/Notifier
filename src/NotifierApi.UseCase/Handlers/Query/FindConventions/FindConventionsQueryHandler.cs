
namespace NotifierApi.UseCase.Handlers.Query.FindConventions
{
    internal sealed class FindConventionsQueryHandler : IRequestHandler<FindConventionsQuery, IReadOnlyList<Convention>>
    {
        readonly IConventionRepository _conventionRepository;

        public FindConventionsQueryHandler(IConventionRepository conventionRepository)
        {
            _conventionRepository = conventionRepository;
        }

        public async Task<IReadOnlyList<Convention>> Handle(FindConventionsQuery query, CancellationToken cancellationToken)
            => await _conventionRepository.FindAllAsync(query.GetExpression(), nameof(Convention.ModificationTime), SortOrder.Asc);
    }
}
