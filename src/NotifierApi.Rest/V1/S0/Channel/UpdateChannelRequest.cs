namespace NotifierApi.Rest.V1.S0
{
    public sealed record UpdateChannelRequest
    (
        long Id,
        string Name,
        string Data,
        Transport Transport
    );
}
