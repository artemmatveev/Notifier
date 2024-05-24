namespace NotifierApi.UseCase.Handlers.Query.GetContent
{
    public sealed record GetTemplateContentQuery(
        long Id
    ) : BaseRequestQuery<Template>, IRequest<Template>;
}
