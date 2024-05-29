namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindEmailMessagesResponse(
        long Id,
        string FromName,
        string FromEmail,
        string ToName,
        string ToEmail,
        string Subject,
        string Body,
        Priority Priority,
        DateTime CreationTime,
        DateTime SentTime,
        DateTime? ReceiveTime
    );
}
