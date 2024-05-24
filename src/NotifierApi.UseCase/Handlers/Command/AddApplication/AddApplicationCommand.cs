namespace NotifierApi.UseCase.Handlers.Command.AddApplication
{
    public sealed record AddApplicationCommand(
        string Name,
        string? Comment
    ) : IRequest<Application>;
}
