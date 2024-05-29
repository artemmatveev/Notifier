namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindNotificationsRequest(
        long ApplicationId,
        string? Name
    );
}
