namespace NotifierApi.Rest.V1.S0.Template
{    
    public sealed record UpdateTemplateContentRequest
    (
        long Id,
        string Body,
        string Subject
    );
}
