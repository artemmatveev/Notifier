namespace NotifierApi.UseCase.Handlers.Query.GetNotification
{
    public sealed record GetNotificationQuery(
        long Id
    ) : BaseRequestQuery<Notification>, IRequest<Notification>;
}
