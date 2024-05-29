using System.Linq.Expressions;
using Iems.Framework.Core.Exceptions;

namespace NotifierApi.DataProvider.Repository
{
    internal sealed class TemplateRepository
        : BaseRepository<Template, NotifierDbContext>, ITemplateRepository
    {
        public TemplateRepository(NotifierDbContext context) : base(context)
        { }

        public async override Task<Template> AddAsync(Template entity,
            Expression<Func<Template, bool>> expression)
        {
            var isExisting = _context.Set<Template>().Where(e => e.NotificationId == entity.NotificationId).All(expression);
            if (isExisting is false)
            {
                throw new ConflictException("Template already added");
            }
            else
            {
                var addEntity = _context.Set<Template>().Add(entity);
                await _context.SaveChangesAsync();
                return addEntity.Entity;
            }
        }

        public async override Task<int> UpdateAsync(long id, Action<Template> action,
            Expression<Func<Template, bool>> expression)
        {
            ArgumentNullException.ThrowIfNull(action);

            var entity = await _context.Set<Template>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null)
            {
                throw new InvalidParameterException($"Template not found by id {id}");
            }

            var isExisting = _context.Set<Template>().Where(e => e.NotificationId == entity.NotificationId).All(expression);
            if (isExisting is false)
            {
                throw new ConflictException("Notification already added");
            }
            else
            {
                action.Invoke(entity);
                _context.Set<Template>().Update(entity);
                return await _context.SaveChangesAsync();
            }
        }
    }
}
