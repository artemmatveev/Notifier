namespace NotifierApi.UseCase.Handlers.Query.FindResources
{
    public sealed record FindResourcesQuery(
        string? Name
    ) : BaseRequestQuery<Resource>, IRequest<IReadOnlyList<Resource>>
    {
        public override Expression<Func<Resource, bool>> GetExpression()
        {
            Expression<Func<Resource, bool>> query = t => true;

            if (!string.IsNullOrEmpty(Name))
                query = query.AndExpression(e => e.Name.ToLower().Contains(Name.ToLower()));

            return query;
        }
    }
}
