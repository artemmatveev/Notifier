namespace NotifierApi.Rest.V1.S0
{
    public sealed record ChangeTemplateStatusRequest
    (
        long Id,
        Status Status
    );
}
