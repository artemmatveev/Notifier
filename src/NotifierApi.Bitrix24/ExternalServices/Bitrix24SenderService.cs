namespace NotifierApi.Bitrix24.ExternalServices
{
    using System.Text.Json;    
    using Domain;    
    using Options;    
    using UseCase.Repository;

    internal class Bitrix24SenderService : IBitrix24SenderService
    {
        readonly INotificationRepository _notificationRepository;
        readonly IOptions<JsonOptions> _jsonOptions;

        public Bitrix24SenderService(INotificationRepository notificationRepository,
            IOptions<JsonOptions> jsonOptions)
        {
            _notificationRepository = notificationRepository;            
            _jsonOptions = jsonOptions;
        }

        public async Task SendBitrix24Async(Bitrix24Message message)
        {
            ArgumentNullException.ThrowIfNull(message);

            var notification = await _notificationRepository
               .GetWithChannelsAsync(n => n.Id == message.NotificationId && n.Status == Status.Enabled);

            var channels = notification.NotificationChannels
                .Select(nc => nc.Channel)
                .Where(ch => ch.Status == Status.Enabled && ch.Transport == Transport.Bitrix24);

            foreach (var channel in channels)
            {
                var data = channel.Data;
                var options = JsonSerializer.Deserialize<Bitrix24Options>(data, _jsonOptions.Value.JsonSerializerOptions);

                ArgumentNullException.ThrowIfNull(options);
                
                var client = new RestClient(new RestClientOptions(options.BaseUrl));
                var request = new RestRequest("im.notify.personal.add", Method.Post)
                        .AddParameter("USER_ID", message.UserId)
                        .AddParameter("MESSAGE", message.Body);

                var response = await client.PostAsync(request);
            }
        }
    }
}
