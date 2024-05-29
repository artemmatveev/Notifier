namespace NotifierApi.Rest.V1.S0
{
    public sealed record GetTemplateResponse
    (
        long Id,
        long NotificationId,
        Transport Transport,
        Lang Lang,
        string Subject,
        string Body,
        string Name,
        string? Comment,        
        Status Status,
        DateTime CreationTime,
        DateTime ModificationTime
    );
}
