namespace NotifierApi.UseCase.Handlers.Command.DeleteNotification
{
    public sealed record DeleteNotificationCommand(
        long Id
    ) : IRequest<Unit>;
}
