namespace NotifierApi.UseCase.Handlers.Command.Transform
{
    public sealed record TransformCommand
    (
        Guid Constant,
        Transport Transport,
        Lang Lang,
        object Payload
    ) : IRequest<string>;
}
