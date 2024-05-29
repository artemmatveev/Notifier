namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindConventionsRequest(
        long? ResourceId,
        long? NotificationId
    );
}
