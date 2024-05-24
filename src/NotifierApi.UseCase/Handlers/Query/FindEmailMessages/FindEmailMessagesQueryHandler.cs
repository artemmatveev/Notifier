namespace NotifierApi.UseCase.Handlers.Query.FindEmailMessages
{
    using UseCase.Model;

    internal sealed class FindEmailMessagesQueryHandler
        : IRequestHandler<FindEmailMessagesQuery, FindEmailMessagesResult>
    {
        readonly IEmailMessageRepository _emailMessageRepository;
        public FindEmailMessagesQueryHandler(IEmailMessageRepository emailMessageRepository)
        {
            _emailMessageRepository = emailMessageRepository;
        }

        public async Task<FindEmailMessagesResult> Handle(FindEmailMessagesQuery query,
            CancellationToken cancellationToken)
        {
            var totalRecords = await _emailMessageRepository.GetTotalRecords(query.GetExpression());

            var result = await _emailMessageRepository.FindAllAsync(
                query.GetExpression(),
                query.PageNumber,
                query.PageSize,
                nameof(EmailMessage.CreationTime),
                SortOrder.Desc);

            return new FindEmailMessagesResult(result, totalRecords);

        }
    }
}
