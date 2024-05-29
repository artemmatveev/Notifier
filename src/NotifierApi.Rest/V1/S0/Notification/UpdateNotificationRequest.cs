namespace NotifierApi.Rest.V1.S0
{
    public sealed record UpdateNotificationRequest
    (
        long Id,
        string Name,
        string? Comment,
        Priority Priority,
        IReadOnlyList<long> ChannelIds
    );
}
