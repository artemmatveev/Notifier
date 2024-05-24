
namespace NotifierApi.UseCase.Handlers.Query.FindNotifications
{
    public sealed record FindNotificationsQuery(
        long ApplicationId,
        string? Name
    ) : BaseRequestQuery<Notification>, IRequest<IReadOnlyList<Notification>>
    {
        public override Expression<Func<Notification, bool>> GetExpression()
        {
            Expression<Func<Notification, bool>> query = t => t.ApplicationId == ApplicationId;

            if (!string.IsNullOrEmpty(Name))
                query = query.AndExpression(e => e.Name.ToLower().Contains(Name.ToLower()));

            return query;
        }
    }
}
