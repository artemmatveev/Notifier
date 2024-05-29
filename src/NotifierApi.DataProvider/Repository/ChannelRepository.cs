namespace NotifierApi.DataProvider.Repository
{
    internal sealed class ChannelRepository
        : BaseRepository<Channel, NotifierDbContext>, IChannelRepository
    {
        public ChannelRepository(NotifierDbContext context) : base(context)
        { }
    }
}
