namespace NotifierApi.Rest.V1.S0
{
    public sealed record ChangeChannelStatusRequest
    (
        long Id,
        Status Status
    );
}
