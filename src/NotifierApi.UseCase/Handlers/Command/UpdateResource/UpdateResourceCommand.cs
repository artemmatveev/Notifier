namespace NotifierApi.UseCase.Handlers.Command.UpdateResource
{
    public sealed record UpdateResourceCommand(
        long Id,
        long? ChatId
    ) : IRequest<Unit>;
}
