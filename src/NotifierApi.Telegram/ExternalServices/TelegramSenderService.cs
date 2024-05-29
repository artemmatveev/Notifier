namespace NotifierApi.Telegram.ExternalServices
{
    using Domain;
    using Options;
    using UseCase.Repository;

    internal class TelegramSenderService : ITelegramSenderService
    {
        readonly INotificationRepository _notificationRepository;
        readonly HttpClient _httpClient;
        readonly IOptions<JsonOptions> _jsonOptions;

        public TelegramSenderService(INotificationRepository notificationRepository,
            HttpClient httpClient, IOptions<JsonOptions> jsonOptions)
        {
            _notificationRepository = notificationRepository;
            _httpClient = httpClient;
            _jsonOptions = jsonOptions;
        }

        public async Task SendTelegramAsync(TelegramMessage message)
        {
            ArgumentNullException.ThrowIfNull(message);

            var notification = await _notificationRepository
                .GetWithChannelsAsync(n => n.Id == message.NotificationId && n.Status == Status.Enabled);

            var channels = notification.NotificationChannels
                .Select(nc => nc.Channel)
                .Where(ch => ch.Status == Status.Enabled && ch.Transport == Transport.Telegram);

            foreach (var channel in channels)
            {
                var data = channel.Data;
                var options = JsonSerializer.Deserialize<TelegramOptions>(data, _jsonOptions.Value.JsonSerializerOptions);

                ArgumentNullException.ThrowIfNull(options);

                var botClient = new TelegramBotClient(options.Token, _httpClient);
                await botClient.SendTextMessageAsync(message.ChatId, message.Body, parseMode: ParseMode.Html);
            }
        }
    }
}
