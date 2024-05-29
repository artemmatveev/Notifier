namespace NotifierApi.DataProvider.Repository
{
    internal sealed class TelegramMessageRepository
        : BaseRepository<TelegramMessage, NotifierDbContext>, ITelegramMessageRepository
    {
        public TelegramMessageRepository(NotifierDbContext context) : base(context)
        { }
    }
}
