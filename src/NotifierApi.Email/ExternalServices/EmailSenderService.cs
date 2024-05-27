namespace NotifierApi.Email.ExternalServices
{
    using Domain;
    using Options;
    using UseCase.Repository;

    internal sealed class EmailSenderService : IEmailSenderService
    {
        readonly INotificationRepository _notificationRepository;
        readonly IOptions<JsonOptions> _jsonOptions;

        public EmailSenderService(INotificationRepository notificationRepository,
            IOptions<JsonOptions> jsonOptions)
        {
            _notificationRepository = notificationRepository;
            _jsonOptions = jsonOptions;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            ArgumentNullException.ThrowIfNull(message);

            var notification = await _notificationRepository
                .GetWithChannelsAsync(n => n.Id == message.NotificationId && n.Status == Status.Enabled);

            var channels = notification.NotificationChannels
                .Select(nc => nc.Channel)
                .Where(ch => ch.Status == Status.Enabled && ch.Transport == Transport.Email);

            foreach (var channel in channels)
            {
                var data = channel.Data;
                var options = JsonSerializer.Deserialize<SmtpOptions>(data, _jsonOptions.Value.JsonSerializerOptions);

                ArgumentNullException.ThrowIfNull(options);

                using (var smtpClient = new SmtpClient()
                {
                    Port = options.Port,
                    DeliveryMethod = options.DeliveryMethod,
                    UseDefaultCredentials = options.UseDefaultCredentials,
                    Host = options.Host,
                    Credentials = new NetworkCredential(options.CredentialsUserName, options.CredentialsPassword),
                    EnableSsl = options.EnableSsl
                })
                {
                    var from = CompileMailAddress(message.FromEmail, message.FromName);
                    var to = CompileMailAddress(message.ToEmail, message.ToName);
                    var priority = GetPriority(message.Priority);

                    var mailMessage = new MailMessage(from, to)
                    {
                        IsBodyHtml = true,
                        Subject = message.Subject,
                        Body = message.Body,
                        Priority = priority
                    };

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }

        private MailAddress CompileMailAddress(string address, string displayName)
            => new MailAddress(address, displayName);

        private MailPriority GetPriority(Priority priority)
            => priority switch
            {
                Priority.Minor or Priority.Lowest => MailPriority.Low,
                Priority.High or Priority.Highest => MailPriority.High,
                _ => MailPriority.Normal,
            };
    }
}
