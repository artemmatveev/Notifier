namespace NotifierApi.UseCase.Handlers.Command.AddNotification
{
    public sealed record AddNotificationCommand(
        long ApplicationId,
        string Name,
        string? Comment,
        Priority Priority,
        IReadOnlyList<long> ChannelIds
    ) : IRequest<Notification>;
}
