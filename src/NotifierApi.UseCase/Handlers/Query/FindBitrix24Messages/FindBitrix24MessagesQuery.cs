using Iems.Framework.Common.Dto;
using NotifierApi.UseCase.Model;

namespace NotifierApi.UseCase.Handlers.Query.FindBitrix24Messages
{
    public sealed record FindBitrix24MessagesQuery()
        : PaginationFilter, IRequest<FindBitrix24MessagesResult>
    {
        public Expression<Func<Bitrix24Message, bool>> GetExpression()
        {
            Expression<Func<Bitrix24Message, bool>> query = t => true;

            return query;
        }
    }
}
