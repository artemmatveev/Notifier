using Iems.Framework.Common.Dto;
using NotifierApi.UseCase.Model;

namespace NotifierApi.UseCase.Handlers.Query.FindEmailMessages
{
    public sealed record FindEmailMessagesQuery()
        : PaginationFilter, IRequest<FindEmailMessagesResult>
    {
        public Expression<Func<EmailMessage, bool>> GetExpression()
        {
            Expression<Func<EmailMessage, bool>> query = t => true;

            return query;
        }
    }
}
