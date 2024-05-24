namespace NotifierApi.UseCase.Handlers.Query.FindChannels
{
    public sealed record FindChannelsQuery(
        string? Name,
        long? NotificationId
        ) : BaseRequestQuery<Channel>, IRequest<IReadOnlyList<Channel>>
    {
        public override Expression<Func<Channel, bool>> GetExpression()
        {
            Expression<Func<Channel, bool>> query = t => true;

            if (!string.IsNullOrEmpty(Name))
                query = query.AndExpression(ch => ch.Name.ToLower().Contains(Name.ToLower()));

            if (NotificationId.HasValue)
                query = query.AndExpression(ch => ch.NotificationChannels.Any(nch => nch.NotificationId == NotificationId));

            return query;
        }
    }
}
