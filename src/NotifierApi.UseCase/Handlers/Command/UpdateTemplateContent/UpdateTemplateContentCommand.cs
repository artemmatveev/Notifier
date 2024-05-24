namespace NotifierApi.UseCase.Handlers.Command.UpdateTemplateContent
{   
    public sealed record UpdateTemplateContentCommand(
        long Id,
        string Subject,
        string Body        
    ) : IRequest<Unit>;
}
