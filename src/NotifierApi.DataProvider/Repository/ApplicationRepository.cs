namespace NotifierApi.DataProvider.Repository
{
    internal sealed class ApplicationRepository
        : BaseRepository<Application, NotifierDbContext>, IApplicationRepository
    {
        public ApplicationRepository(NotifierDbContext context) : base(context)
        { }
    }
}
