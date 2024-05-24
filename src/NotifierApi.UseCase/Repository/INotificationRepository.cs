namespace NotifierApi.UseCase.Repository
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<Notification> GetWithChannelsAsync(Expression<Func<Notification, bool>> expression, bool asNoTracking = false);
    }
}
