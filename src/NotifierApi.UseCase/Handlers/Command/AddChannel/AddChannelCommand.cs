namespace NotifierApi.UseCase.Handlers.Command.AddChannel
{
    public sealed record AddChannelCommand(
        string Name,
        string Data,
        Transport Transport
    ) : IRequest<Channel>;
}
