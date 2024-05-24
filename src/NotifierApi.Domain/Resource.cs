namespace NotifierApi.Domain
{
    public sealed class Resource : BaseAuditableEntity<long>
    {
        public Resource(int? staffId, string name, string? email,
            string? username, long? chatId)
        {
            CheckParams(name);

            StaffId = staffId;
            Name = name;
            Email = email;
            Username = username;
            ChatId = chatId;
        }

        public int? StaffId { get; private set; }
        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string? Username { get; private set; }
        public long? ChatId { get; private set; } // https://core.telegram.org/bots/api#chat
        public long? Bitrix24UserId { get; private set; }

        public static Resource Create(int? staffId, string name, string? email,
            string? username, long? chatId)
                => new Resource(staffId, name, email, username, chatId);

        public void Update(long? chatId)
        {
            ChatId = chatId;
            UpdateModificationTime();
        }

        private void UpdateModificationTime() => ModificationTime = DateTime.Now;

        private void CheckParams(string name)
        {
            if (name is null)
            {
                throw new InvalidParameterException($"{nameof(name)} is requered");
            }
        }
    }
}
