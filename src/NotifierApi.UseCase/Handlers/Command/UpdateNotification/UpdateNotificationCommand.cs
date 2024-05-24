namespace NotifierApi.UseCase.Handlers.Command.UpdateNotification
{
    public sealed record UpdateNotificationCommand(
        long Id,
        string Name,
        string? Comment,
        Priority Priority,
        IReadOnlyList<long> ChannelIds
    ) : IRequest<Unit>;
}
