namespace NotifierApi.Domain
{
    public sealed class Notification : BaseAuditableEntity<long>
    {
        public Notification(long applicationId, Priority priority,
            string name, string? comment)
        {
            CheckParams(name);

            ApplicationId = applicationId;
            Constant = Guid.NewGuid();
            Priority = priority;
            Name = name;
            Comment = comment;
            Status = Status.Enabled;
        }

        public long ApplicationId { get; private set; }
        public Guid Constant { get; private set; }
        public Priority Priority { get; private set; }
        public string Name { get; private set; }
        public string? Comment { get; private set; }
        public Status Status { get; private set; }


        public List<NotificationChannel> NotificationChannels { get; } = new();


        public static Notification Create(Application application,
            Priority priority, string name, string? comment,
            IReadOnlyList<Channel> channels)
        {
            var notification = new Notification(application.Id, priority, name, comment);
            foreach (var channel in channels)
            {
                notification.LinkToChannel(channel.Id);
            }

            return notification;
        }


        public void Update(Priority priority, string name, string? comment,
            IReadOnlyList<Channel> channels)
        {
            CheckParams(name);
            UpdateModificationTime();

            Priority = priority;
            Name = name;
            Comment = comment;

            NotificationChannels.Clear();
            foreach (var channel in channels)
            {
                LinkToChannel(channel.Id);
            }
        }

        public void Delete() => ChangeStatus(Status.Deleted);

        public void ChangeStatus(Status status)
        {
            Status = status;
            UpdateModificationTime();
        }

        private void CheckParams(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidParameterException($"{nameof(name)} is requered");
            }
        }

        private void UpdateModificationTime()
            => ModificationTime = DateTime.Now;

        private void LinkToChannel(long channelId)
            => NotificationChannels.Add(new NotificationChannel(Id, channelId));
    }
}
