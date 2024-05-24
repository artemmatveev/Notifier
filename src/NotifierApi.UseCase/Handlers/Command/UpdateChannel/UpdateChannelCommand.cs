namespace NotifierApi.UseCase.Handlers.Command.UpdateChannel
{
    public sealed record UpdateChannelCommand(
            long Id,
            string Name,
            string Data,
            Transport Transport
        ) : IRequest<Unit>;
}
