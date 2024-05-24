namespace NotifierApi.UseCase.Handlers.Query.GetTemplate
{
    public sealed record GetTemplateQuery(
        long Id
    ) : BaseRequestQuery<Template>, IRequest<Template>;
}
