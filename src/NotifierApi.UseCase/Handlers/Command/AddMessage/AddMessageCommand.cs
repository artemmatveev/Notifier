namespace NotifierApi.UseCase.Handlers.Command.AddMessage
{
    public sealed record AddMessageCommand(
        Guid Constant,
        object? Payload,
        string? FromName
    ) : IRequest<Unit>;
}
