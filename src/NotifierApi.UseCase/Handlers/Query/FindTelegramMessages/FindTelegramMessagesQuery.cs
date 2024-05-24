using Iems.Framework.Common.Dto;
using NotifierApi.UseCase.Model;

namespace NotifierApi.UseCase.Handlers.Query.FindTelegramMessages
{
    public sealed record FindTelegramMessagesQuery()
       : PaginationFilter, IRequest<FindTelegramMessagesResult>
    {
        public Expression<Func<TelegramMessage, bool>> GetExpression()
        {
            Expression<Func<TelegramMessage, bool>> query = t => true;

            return query;
        }
    }
}
