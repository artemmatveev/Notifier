namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddNotificationRequest
    (
        long ApplicationId,
        string Name,
        string? Comment,
        Priority Priority,
        IReadOnlyList<long> ChannelIds
    );
}
