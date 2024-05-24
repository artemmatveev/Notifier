using Iems.Framework.Core.Exceptions;

namespace NotifierApi.UseCase.Handlers.Command.AddConvention
{
    internal sealed class AddConventionCommandHandler : IRequestHandler<AddConventionCommand, Convention>
    {
        readonly IConventionRepository _conventionRepository;
        readonly INotificationRepository _notificationRepository;
        readonly IResourceRepository _resourceRepository;

        public AddConventionCommandHandler(IConventionRepository conventionRepository,
            INotificationRepository notificationRepository, IResourceRepository resourceRepository)
        {
            _conventionRepository = conventionRepository;
            _notificationRepository = notificationRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<Convention> Handle(AddConventionCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.FindAsync(e => e.Id == request.NotificationId);
            if (notification is null)
                throw new BusinessRuleException("Notification don't find");

            var resource = await _resourceRepository.FindAsync(e => e.Id == request.ResourceId);
            if (resource is null)
                throw new BusinessRuleException("Resource don't find");

            var convention = Convention.Create(request.NotificationId, request.ResourceId);

            return await _conventionRepository.AddAsync(convention,
                e => e.NotificationId != notification.Id && e.ResourceId != resource.Id);
        }
    }
}
