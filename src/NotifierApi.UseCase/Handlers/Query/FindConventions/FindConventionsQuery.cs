namespace NotifierApi.UseCase.Handlers.Query.FindConventions
{
    public sealed record FindConventionsQuery(
        long? ResourceId,
        long? NotificationId
        ) : BaseRequestQuery<Convention>, IRequest<IReadOnlyList<Convention>>
    {
        public override Expression<Func<Convention, bool>> GetExpression()
        {
            Expression<Func<Convention, bool>> query = t => true;
            
            if (ResourceId.HasValue)
                query = query.AndExpression(c => c.ResourceId == ResourceId);

            if (NotificationId.HasValue)
                query = query.AndExpression(c => c.NotificationId == NotificationId);

            return query;
        }
    }
}
