namespace NotifierApi.Rest.V1.S0
{
    public sealed record UpdateResourceRequest
    (
        long Id,
        long? ChatId
    );
}
