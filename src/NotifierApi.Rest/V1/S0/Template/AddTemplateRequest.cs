namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddTemplateRequest
    (
        long NotificationId,
        Transport Transport,
        Lang Lang,
        string Subject,
        string Body,
        string Name,
        string? Comment
    );
}
