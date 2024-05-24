namespace NotifierApi.UseCase.Handlers.Query.GetResource
{
    public sealed record GetResourceQuery(
        long Id
    ) : BaseRequestQuery<Resource>, IRequest<Resource>;
}
