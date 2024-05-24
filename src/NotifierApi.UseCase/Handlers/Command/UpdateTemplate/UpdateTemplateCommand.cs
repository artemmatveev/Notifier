namespace NotifierApi.UseCase.Handlers.Command.UpdateTemplate
{
    public sealed record UpdateTemplateCommand(
        long Id,
        string Name,
        string? Comment,
        Transport Transport,
        Lang Lang          
    ) : IRequest<Unit>;
}
