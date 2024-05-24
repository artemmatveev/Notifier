namespace NotifierApi.UseCase.Handlers.Command.AddMessage
{
    using Iems.Framework.Core.Exceptions;
    using TextToText.Interpreter;

    internal sealed class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, Unit>
    {
        readonly INotificationRepository _notificationRepository;
        readonly ITemplateRepository _templateRepository;
        readonly IEmailMessageRepository _emailMessageRepository;
        readonly IBitrix24MessageRepository _bitrix24MessageRepository;
        readonly ITelegramMessageRepository _telegramMessageRepository;
        readonly IOptionsSnapshot<Options.EmailOptions> _emailOptions;

        public AddMessageCommandHandler(
            INotificationRepository notificationRepository,
            ITemplateRepository templateRepository,
            IEmailMessageRepository emailMessageRepository,
            IBitrix24MessageRepository bitrix24MessageRepository,
            ITelegramMessageRepository telegramMessageRepository,
            IOptionsSnapshot<Options.EmailOptions> emailOptions)
        {
            _notificationRepository = notificationRepository;
            _templateRepository = templateRepository;
            _emailMessageRepository = emailMessageRepository;
            _bitrix24MessageRepository = bitrix24MessageRepository;
            _telegramMessageRepository = telegramMessageRepository;
            _emailOptions = emailOptions;
        }

        public async Task<Unit> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetAsync(n => n.Constant == request.Constant && n.Status == Status.Enabled);

            var templates = await _templateRepository.FindAllAsync(t => t.NotificationId == notification.Id && t.Status == Status.Enabled);
            if (!templates.Any()) throw new BusinessRuleException("Templates don't find");

            var fromEmail = _emailOptions.Value.FromEmail;
            var fromName = request.FromName ?? _emailOptions.Value.FromName;
            var payload = request.Payload is null ? "{}" : request.Payload.ToString();

            foreach (var template in templates)
            {
                using (var doc = JsonDocument.Parse(payload))
                {
                    var context = new Context(doc);
                    var subjectExpression = new Expression(template.Subject);
                    var bodyExpression = new Expression(template.Body);
                    var subject = subjectExpression.Interpret(context).Result;
                    var body = bodyExpression.Interpret(context).Result;

                    switch (template.Transport)
                    {
                        case Transport.Email:
                            await _emailMessageRepository.AddCollectionAsync("EXEC [dbo].[add_email_messages] {0}, {1}, {2}, {3}, {4}, {5}",
                                notification.Id, fromEmail, fromName, subject, body, (int)notification.Priority);
                            break;
                        case Transport.Telegram:
                            await _telegramMessageRepository.AddCollectionAsync("EXEC [dbo].[add_telegram_messages] {0}, {1}, {2}",
                                notification.Id, body, (int)notification.Priority);
                            break;
                        case Transport.Bitrix24:
                            await _bitrix24MessageRepository.AddCollectionAsync("EXEC [dbo].[add_bitrix24_messages] {0}, {1}, {2}",
                                notification.Id, body, (int)notification.Priority);
                            break;
                        case Transport.Push:
                            break;
                        default:
                            break;
                    }
                }
            }

            return Unit.Value;
        }
    }
}
