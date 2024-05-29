namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindConventionsResponse(
        long NotificationId,
        long ResourceId,
        bool Enabled
    );
}
