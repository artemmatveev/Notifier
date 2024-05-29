namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindChannelsRequest(
        string? Name,
        long? NotificationId
    );
}
