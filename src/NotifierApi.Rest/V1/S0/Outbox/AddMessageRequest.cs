namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddMessageRequest
    (
        Guid Constant,
        object? Payload,
        string? FromName
    );
}
