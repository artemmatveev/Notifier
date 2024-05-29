namespace NotifierApi.Rest.V1.S0
{
    public sealed record FindBitrix24MessagesResponse(
        long Id,
        string Body,
        Priority Priority,
        DateTime CreationTime,
        DateTime SentTime,
        DateTime? ReceiveTime
    );
}
