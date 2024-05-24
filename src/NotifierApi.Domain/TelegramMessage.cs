namespace NotifierApi.Domain
{
    public sealed class TelegramMessage : Outbox
    {
        public TelegramMessage(long notificationId, long resourceId,
            string body, Priority priority, long chatId) : base(notificationId, resourceId, body, priority)
        {
            ChatId = chatId;
        }

        public long ChatId { get; private set; }
        public DateTime? ReceiveTime { get; private set; }

        public static TelegramMessage Create(long notificationId, long resourceId,
            string body, Priority priority, long chatId)
           => new TelegramMessage(
               notificationId,
               resourceId,
               body,
               priority,
               chatId);


        public void Receive() => ReceiveTime = DateTime.Now;
    }
}
