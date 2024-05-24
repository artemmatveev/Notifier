namespace NotifierApi.UseCase.Handlers.Command.DeleteChannel
{
    public sealed record DeleteChannelCommand(
        long Id
    ) : IRequest<Unit>;
}
