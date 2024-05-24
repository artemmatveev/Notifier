namespace NotifierApi.UseCase.Handlers.Query.GetApplication
{
    public sealed record GetApplicationQuery(
        long Id
    ) : IRequest<Application>
    { }
}
