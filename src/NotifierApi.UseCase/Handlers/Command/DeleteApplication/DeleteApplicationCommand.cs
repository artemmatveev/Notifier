namespace NotifierApi.UseCase.Handlers.Command.DeleteApplication
{
    public sealed record DeleteApplicationCommand(
        long Id
    ) : IRequest<Unit>;
}
