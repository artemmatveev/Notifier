namespace NotifierApi.Domain
{
    public sealed class Channel : BaseAuditableEntity<long>
    {
        public Channel(string name, string data, Transport transport)
        {
            CheckParams(name, data);

            Name = name;
            Data = data;
            Transport = transport;
            Status = Status.Enabled;
        }

        public string Name { get; private set; }
        public string Data { get; private set; }
        public Transport Transport { get; set; }
        public Status Status { get; private set; }


        public List<NotificationChannel> NotificationChannels { get; } = new();


        public static Channel Create(string name, string data, Transport transport)
            => new Channel(name, data, transport);

        public void Update(string name, string data, Transport transport)
        {
            CheckParams(name, data);
            UpdateModificationTime();

            Name = name;
            Data = data;
            Transport = transport;
        }

        public void Delete() => ChangeStatus(Status.Deleted);
        public void ChangeStatus(Status status)
        {
            Status = status;
            UpdateModificationTime();
        }

        private void CheckParams(string name, string data)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidParameterException($"{nameof(name)} is requered");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new InvalidParameterException($"{nameof(data)} is requered");
            }
        }

        private void UpdateModificationTime()
            => ModificationTime = DateTime.Now;
    }
}
