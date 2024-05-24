namespace NotifierApi.Domain
{
    public sealed class NotificationChannel
    {
        public NotificationChannel(long notificationId, long channelId)
        {
            NotificationId = notificationId;
            ChannelId = channelId;
        }

        public long NotificationId { get; private set; }
        public long ChannelId { get; private set; }

        public Notification Notification { get; private set; } = null!;
        public Channel Channel { get; private set; } = null!;
    }
}
