namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddApplicationResponse
    (
        long Id,
        string Name,
        string? Comment,        
        Status Status,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
