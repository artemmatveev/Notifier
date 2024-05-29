namespace NotifierApi.Rest.V1.S0
{
    public sealed record ChangeApplicationStatusRequest
    (
        long Id,
        Status Status
    );
}
