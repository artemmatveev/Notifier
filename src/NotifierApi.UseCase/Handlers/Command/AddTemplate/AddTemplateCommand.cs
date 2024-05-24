namespace NotifierApi.UseCase.Handlers.Command.AddTemplate
{
    public sealed record AddTemplateCommand(
        long NotificationId,
        Transport Transport,
        Lang Lang,
        string Subject,
        string Body,
        string Name,
        string? Comment
    ) : IRequest<Template>;
}
