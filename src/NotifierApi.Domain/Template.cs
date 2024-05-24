namespace NotifierApi.Domain
{
    public sealed class Template : BaseAuditableEntity<long>
    {
        public Template(long notificationId, Transport transport, Lang lang, string subject,
            string body, string name, string? comment)
        {
            CheckName(name);
            CheckContent(subject, body);

            NotificationId = notificationId;
            Transport = transport;
            Lang = lang;
            Subject = subject;
            Body = body;
            Name = name;
            Comment = comment;
            Status = Status.Enabled;
        }

        public long NotificationId { get; private set; }
        public Transport Transport { get; private set; }
        public Lang Lang { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string Name { get; private set; }
        public string? Comment { get; private set; }
        public Status Status { get; private set; }

        public static Template Create(Notification notification, Transport transport, Lang lang, string subject,
            string body, string name, string? comment)
                => new Template(notification.Id, transport, lang, subject, body, name, comment);


        public void UpdateContent(string subject, string body)
        {
            CheckContent(subject, body);
            Subject = subject;
            Body = body;
            UpdateModificationTime();
        }


        public void Update(Transport transport, Lang lang,
            string name, string? comment)
        {
            CheckName(name);
            Transport = transport;
            Lang = lang;
            Name = name;
            Comment = comment;
            UpdateModificationTime();
        }

        public void Delete() => ChangeStatus(Status.Deleted);

        public void ChangeStatus(Status status)
        {
            Status = status;
            UpdateModificationTime();
        }

        private void UpdateModificationTime() => ModificationTime = DateTime.Now;


        private void CheckName(string name)
        {
            if (name is null)
                throw new InvalidParameterException($"{nameof(name)} is requered");
        }

        private void CheckContent(string subject, string body)
        {
            if (subject is null)
                throw new InvalidParameterException($"{nameof(subject)} is requered");

            if (body is null)
                throw new InvalidParameterException($"{nameof(body)} is requered");
        }
    }
}
