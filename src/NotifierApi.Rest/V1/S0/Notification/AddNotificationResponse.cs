namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddNotificationResponse
    (
        long Id,
        long ApplicationId,
        Guid Constant,
        Priority Priority,
        string Name,
        string? Comment,        
        Status Status,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
