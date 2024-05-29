namespace NotifierApi.Rest.V1.S0
{
    public sealed record UpdateApplicationRequest
    (
        long Id,
        string Name,
        string? Comment
    );
}
