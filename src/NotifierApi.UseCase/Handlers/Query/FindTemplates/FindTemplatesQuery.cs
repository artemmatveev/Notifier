namespace NotifierApi.UseCase.Handlers.Query.FindTemplates
{
    public sealed record FindTemplatesQuery(
        long NotificationId,
        string? Name
    ) : BaseRequestQuery<Template>, IRequest<IReadOnlyList<Template>>
    {
        public override Expression<Func<Template, bool>> GetExpression()
        {
            Expression<Func<Template, bool>> query = t => t.NotificationId == NotificationId;

            if (!string.IsNullOrEmpty(Name))
                query = query.AndExpression(e => e.Name.ToLower().Contains(Name.ToLower()));

            return query;
        }
    }
}
