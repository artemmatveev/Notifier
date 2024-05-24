namespace NotifierApi.Domain
{
    public sealed class Application : BaseAuditableEntity<long>
    {
        public Application(string name, string? comment)
        {
            CheckParams(name);

            Name = name;
            Comment = comment;
            Status = Status.Enabled;
        }

        public string Name { get; private set; }
        public string? Comment { get; private set; }
        public Status Status { get; private set; }


        public static Application Create(string name, string? comment)
            => new Application(name, comment);

        public void Update(string name, string? comment)
        {
            CheckParams(name);
            UpdateModificationTime();

            Name = name;
            Comment = comment;
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
    }
}
