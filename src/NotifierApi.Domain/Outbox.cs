namespace NotifierApi.Domain
{
    public abstract class Outbox : BaseEntity<long>
    {
        public Outbox(long notificationId, long resourceId,
            string body, Priority priority)
        {
            CheckParams(body);

            NotificationId = notificationId;
            ResourceId = resourceId;
            Body = body;
            Priority = priority;
            CreationTime = DateTime.Now;
        }

        public long NotificationId { get; private set; }
        public long ResourceId { get; private set; }
        public string Body { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime? SentTime { get; private set; }

        private void CheckParams(string body)
        {
            if (body is null)
            {
                throw new InvalidParameterException($"{nameof(body)} is requered");
            }
        }

        public void Send() => SentTime = DateTime.Now;
    }
}
