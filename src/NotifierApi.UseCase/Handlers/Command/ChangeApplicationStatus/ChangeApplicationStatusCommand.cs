namespace NotifierApi.UseCase.Handlers.Command.ChangeApplicationStatus
{
    public sealed record ChangeApplicationStatusCommand(
       long Id,
       Status Status
   ) : IRequest<Unit>;
}
