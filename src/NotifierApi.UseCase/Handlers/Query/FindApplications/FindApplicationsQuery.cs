namespace NotifierApi.UseCase.Handlers.Query.FindApplications
{
    public sealed record FindApplicationsQuery(
        string? Name
        ) : BaseRequestQuery<Application>, IRequest<IReadOnlyList<Application>>
    {
        public override Expression<Func<Application, bool>> GetExpression()
        {
            Expression<Func<Application, bool>> query = t => true;

            if (!string.IsNullOrEmpty(Name))
                query = query.AndExpression(e => e.Name.ToLower().Contains(Name.ToLower()));

            return query;
        }
    }
}
