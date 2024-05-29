namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddChannelResponse
    (
        long Id,
        string Name,
        Transport Transport,
        string Data,        
        Status Status,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
