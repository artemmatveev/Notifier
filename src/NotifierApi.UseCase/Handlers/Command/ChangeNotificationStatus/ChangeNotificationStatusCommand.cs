namespace NotifierApi.UseCase.Handlers.Command.ChangeNotificationStatus
{
    public sealed record ChangeNotificationStatusCommand(
           long Id,
           Status Status
       ) : IRequest<Unit>;
}
