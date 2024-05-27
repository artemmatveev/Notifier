namespace NotifierApi.Email.ExternalServices
{
    using Domain;
    using Options;
    using UseCase.Repository;

    internal sealed class SmtpMailKitSenderService : IEmailSenderService
    {
        readonly INotificationRepository _notificationRepository;
        readonly IOptions<JsonOptions> _jsonOptions;

        public SmtpMailKitSenderService(INotificationRepository notificationRepository,
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

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(options.Host, options.Port, SecureSocketOptions.None);
                    await client.AuthenticateAsync(options.CredentialsUserName, options.CredentialsPassword);

                    await client.SendAsync(GetMimeMessage(message));
                    await client.DisconnectAsync(true);
                }
            }
        }

        private MimeMessage GetMimeMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage()
            {
                Body = CreateBody(message),
                Subject = message.Subject
            };
            mimeMessage.From.Add(new MailboxAddress(message.FromName, message.FromEmail));
            mimeMessage.To.Add(new MailboxAddress(message.ToName, message.ToEmail));

            return mimeMessage;
        }

        private MimeEntity CreateBody(EmailMessage message)
        {
            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = message.Body
            };
            return bodyBuilder.ToMessageBody();
        }
    }
}
