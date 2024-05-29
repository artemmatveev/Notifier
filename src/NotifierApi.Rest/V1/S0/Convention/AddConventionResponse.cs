namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddConventionResponse
    (
        long Id,
        long NotificationId,
        long ResourceId,
        bool Enabled,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
