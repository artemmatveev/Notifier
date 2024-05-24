namespace NotifierApi.UseCase.Handlers.Query.GetChannel
{
    public sealed record GetChannelQuery(
        long Id
    ) : IRequest<Channel>
    { }
}
