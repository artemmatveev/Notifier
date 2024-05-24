namespace NotifierApi.Domain
{
    public sealed class EmailMessage : Outbox
    {
        public EmailMessage(long notificationId, long resourceId, string fromName, string fromEmail, string toName, string toEmail,
            string subject, string body, Priority priority) : base(notificationId, resourceId, body, priority)
        {
            CheckParams(subject, fromName, fromEmail, toName, toEmail);

            FromName = fromName;
            FromEmail = fromEmail;
            ToName = toName;
            ToEmail = toEmail;
            Subject = subject;
        }

        public string FromName { get; private set; }
        public string FromEmail { get; private set; }
        public string ToName { get; private set; }
        public string ToEmail { get; private set; }
        public string Subject { get; private set; }
        public DateTime? ReceiveTime { get; private set; }

        public static EmailMessage Create(long notificationId, long resourceId, string fromName,
            string fromEmail, string toName, string toEmail, string subject, string body, Priority priority)
           => new EmailMessage(
               notificationId,
               resourceId,
               fromName,
               fromEmail,
               toName,
               toEmail,
               subject,
               body,
               priority);

        public void Receive()
        {
            ReceiveTime = DateTime.Now;
        }

        private void CheckParams(string subject, string fromName, string fromEmail,
            string toName, string toEmail)
        {
            if (subject is null)
            {
                throw new InvalidParameterException($"{nameof(subject)} is requered");
            }

            CheckParams(fromName, fromEmail);
            CheckParams(toName, toEmail);
        }

        private void CheckParams(string name, string email)
        {
            if (name is null)
            {
                throw new InvalidParameterException($"{nameof(name)} is requered");
            }

            if (email is null)
            {
                throw new InvalidParameterException($"{nameof(name)} is requered");
            }
        }
    }
}
