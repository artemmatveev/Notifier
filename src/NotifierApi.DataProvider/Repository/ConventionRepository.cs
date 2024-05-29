namespace NotifierApi.DataProvider.Repository
{
    internal sealed class ConventionRepository
         : BaseRepository<Convention, NotifierDbContext>, IConventionRepository
    {
        public ConventionRepository(NotifierDbContext context) : base(context)
        { }
    }
}
