namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindTemplatesRequest(
        long NotificationId,
        string? Name
    );
}
