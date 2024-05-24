namespace NotifierApi.UseCase.Handlers.Command.ChangeTemplateStatus
{
    public sealed record ChangeTemplateStatusCommand(
          long Id,
          Status Status
      ) : IRequest<Unit>;
}
