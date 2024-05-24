namespace NotifierApi.UseCase.Handlers.Command.EnableConvention
{
    public sealed record EnableConventionCommand(
        long Id,
        bool Enabled
    ) : IRequest<Unit>;
}
