namespace NotifierApi.UseCase.Handlers.Query.FindTelegramMessages
{
    using UseCase.Model;

    internal sealed class FindTelegramMessagesQueryHandler
        : IRequestHandler<FindTelegramMessagesQuery, FindTelegramMessagesResult>
    {
        readonly ITelegramMessageRepository _telegramMessageRepository;
        public FindTelegramMessagesQueryHandler(ITelegramMessageRepository telegramMessageRepository)
        {
            _telegramMessageRepository = telegramMessageRepository;
        }

        public async Task<FindTelegramMessagesResult> Handle(FindTelegramMessagesQuery query,
            CancellationToken cancellationToken)
        {
            var totalRecords = await _telegramMessageRepository.GetTotalRecords(query.GetExpression());

            var telegramMessages = await _telegramMessageRepository.FindAllAsync(
                query.GetExpression(),
                query.PageNumber,
                query.PageSize,
                nameof(EmailMessage.CreationTime),
                SortOrder.Desc);

            return new FindTelegramMessagesResult(telegramMessages, totalRecords);
        }
    }
}
