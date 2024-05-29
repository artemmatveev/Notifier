namespace NotifierApi.Rest.V1.S0
{
    public sealed record UpdateTemplateRequest
    (
        long Id,
        string Name,
        string? Comment,
        Transport Transport,
        Lang Lang
    );
}
