namespace NotifierApi.UseCase.Handlers.Query.FindBitrix24Messages
{    
    using UseCase.Model;
   
    internal sealed class FindBitrix24MessagesQueryHandler
        : IRequestHandler<FindBitrix24MessagesQuery, FindBitrix24MessagesResult>
    {
        readonly IBitrix24MessageRepository _bitrix24MessageRepository;
        public FindBitrix24MessagesQueryHandler(IBitrix24MessageRepository bitrix24MessageRepository)
        {
            _bitrix24MessageRepository = bitrix24MessageRepository;
        }

        public async Task<FindBitrix24MessagesResult> Handle(FindBitrix24MessagesQuery query,
            CancellationToken cancellationToken)
        {
            var totalRecords = await _bitrix24MessageRepository.GetTotalRecords(query.GetExpression());

            var result = await _bitrix24MessageRepository.FindAllAsync(
                query.GetExpression(),
                query.PageNumber,
                query.PageSize,
                nameof(EmailMessage.CreationTime),
                SortOrder.Desc);

            return new FindBitrix24MessagesResult(result, totalRecords);

        }
    }
}
