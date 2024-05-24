namespace NotifierApi.UseCase.Handlers.Command.ChangeChannelStatus
{
    public sealed record ChangeChannelStatusCommand(
           long Id,
           Status Status
       ) : IRequest<Unit>;
}
