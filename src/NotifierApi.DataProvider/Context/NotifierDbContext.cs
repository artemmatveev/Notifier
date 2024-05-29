namespace NotifierApi.DataProvider.Context
{
    internal sealed partial class NotifierDbContext : DbContext
    {
        public NotifierDbContext(DbContextOptions<NotifierDbContext> options)
            : base(options) { }
    }
}
