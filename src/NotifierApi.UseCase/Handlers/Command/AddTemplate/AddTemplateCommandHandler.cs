using Iems.Framework.Core.Exceptions;

namespace NotifierApi.UseCase.Handlers.Command.AddTemplate
{
    internal sealed class AddTemplateCommandHandler : IRequestHandler<AddTemplateCommand, Template>
    {
        readonly ITemplateRepository _templateRepository;
        readonly INotificationRepository _notificationRepository;

        public AddTemplateCommandHandler(ITemplateRepository templateRepository,
            INotificationRepository notificationRepository)
        {
            _templateRepository = templateRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<Template> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.FindAsync(e => e.Id == request.NotificationId);
            if (notification is null)
                throw new BusinessRuleException("Notification don't find");

            var template = Template.Create(notification, request.Transport,
                request.Lang, request.Subject, request.Body, request.Name, request.Comment);

            return await _templateRepository.AddAsync(template, e => e.Name != template.Name);
        }
    }
}
