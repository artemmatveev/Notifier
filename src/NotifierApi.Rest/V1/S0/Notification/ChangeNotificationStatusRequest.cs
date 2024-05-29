namespace NotifierApi.Rest.V1.S0
{
    public sealed record ChangeNotificationStatusRequest
    (
        long Id,
        Status Status
    );
}
