using System.Linq.Expressions;
using Iems.Framework.Core.Exceptions;

namespace NotifierApi.DataProvider.Repository
{
    internal sealed class NotificationRepository
        : BaseRepository<Notification, NotifierDbContext>, INotificationRepository
    {
        public NotificationRepository(NotifierDbContext context) : base(context)
        { }

        public async override Task<Notification> AddAsync(Notification entity,
            Expression<Func<Notification, bool>> expression)
        {
            var isExisting = _context.Set<Notification>().Where(e => e.ApplicationId == entity.ApplicationId).All(expression);
            if (isExisting is false)
            {
                throw new ConflictException("Notification already added");
            }
            else
            {
                var addEntity = _context.Set<Notification>().Add(entity);
                await _context.SaveChangesAsync();
                return addEntity.Entity;
            }
        }

        public async override Task<int> UpdateAsync(long id, Action<Notification> action,
            Expression<Func<Notification, bool>> expression)
        {
            ArgumentNullException.ThrowIfNull(action);

            var entity = await _context.Set<Notification>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null)
            {
                throw new InvalidParameterException($"Notification not found by id {id}");
            }

            var isExisting = _context.Set<Notification>().Where(e => e.ApplicationId == entity.ApplicationId).All(expression);
            if (isExisting is false)
            {
                throw new ConflictException("Notification already added");
            }
            else
            {
                action.Invoke(entity);
                _context.Set<Notification>().Update(entity);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<Notification> GetWithChannelsAsync(Expression<Func<Notification, bool>> expression,
            bool asNoTracking = false)
        {
            IQueryable<Notification> set = _context.Set<Notification>()
                .Include(e => e.NotificationChannels).ThenInclude(e => e.Channel);

            if (asNoTracking)
            {
                set.AsNoTracking();
            }

            var entity = await set.FirstOrDefaultAsync(expression);

            if (entity is null)
            {
                throw new InvalidParameterException("Notification not found by speciffied parameters");
            }
            else
            {
                return entity;
            }
        }               
    }
}
