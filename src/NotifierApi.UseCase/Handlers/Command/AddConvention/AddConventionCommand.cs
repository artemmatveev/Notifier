namespace NotifierApi.UseCase.Handlers.Command.AddConvention
{
    public sealed record AddConventionCommand(
        long NotificationId,
        long ResourceId
    ) : IRequest<Convention>;
}
