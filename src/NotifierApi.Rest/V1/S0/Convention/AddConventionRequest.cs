namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddConventionRequest
    (
        long NotificationId,
        long ResourceId
    );
}
