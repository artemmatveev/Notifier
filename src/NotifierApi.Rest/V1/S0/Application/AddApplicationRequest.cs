namespace NotifierApi.Rest.V1.S0
{
    public sealed record AddApplicationRequest
    (
        string Name,
        string? Comment
    );
}
