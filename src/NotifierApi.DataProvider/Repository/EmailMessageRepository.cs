namespace NotifierApi.DataProvider.Repository
{
    internal sealed class EmailMessageRepository
        : BaseRepository<EmailMessage, NotifierDbContext>, IEmailMessageRepository
    {
        public EmailMessageRepository(NotifierDbContext context) : base(context)
        { }
    }
}
