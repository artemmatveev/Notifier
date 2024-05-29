namespace NotifierApi.DataProvider.Repository
{
    internal sealed class ResourceRepository
        : BaseRepository<Resource, NotifierDbContext>, IResourceRepository
    {
        public ResourceRepository(NotifierDbContext context) : base(context)
        { }
    }
}
