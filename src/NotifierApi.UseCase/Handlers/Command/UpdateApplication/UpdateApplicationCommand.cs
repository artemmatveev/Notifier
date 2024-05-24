namespace NotifierApi.UseCase.Handlers.Command.UpdateApplication
{
    public sealed record UpdateApplicationCommand(
        long Id,
        string Name,
        string? Comment
    ) : IRequest<Unit>;
}
