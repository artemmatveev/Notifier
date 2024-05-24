namespace NotifierApi.UseCase.Handlers.Command.DeleteTemplate
{
    public sealed record DeleteTemplateCommand(
        long Id
    ) : IRequest<Unit>;
}
