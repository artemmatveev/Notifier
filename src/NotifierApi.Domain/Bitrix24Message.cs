namespace NotifierApi.Domain
{
    public sealed class Bitrix24Message : Outbox
    {
        public Bitrix24Message(long notificationId, long resourceId,
            string body, Priority priority, long userId) : base(notificationId, resourceId, body, priority)
        {
            UserId = userId;
        }

        public long UserId { get; private set; }
        public DateTime? ReceiveTime { get; private set; }

        public static TelegramMessage Create(long notificationId, long resourceId,
            string body, Priority priority, long userId)
           => new TelegramMessage(
               notificationId,
               resourceId,
               body,
               priority,
               userId);

        public void Receive() => ReceiveTime = DateTime.Now;
    }
}
