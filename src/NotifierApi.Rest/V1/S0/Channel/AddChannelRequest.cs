namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddChannelRequest(
        string Name,
        string Data,
        Transport Transport
    );
}
