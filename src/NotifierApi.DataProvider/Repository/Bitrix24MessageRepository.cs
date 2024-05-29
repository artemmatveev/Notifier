namespace NotifierApi.DataProvider.Repository
{
    internal sealed class Bitrix24MessageRepository
        : BaseRepository<Bitrix24Message, NotifierDbContext>, IBitrix24MessageRepository
    {
        public Bitrix24MessageRepository(NotifierDbContext context) : base(context) 
        { }
    }
}
