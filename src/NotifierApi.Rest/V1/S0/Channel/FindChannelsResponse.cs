namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindChannelsResponse
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
