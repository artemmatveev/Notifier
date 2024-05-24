namespace NotifierApi.Domain
{
    public sealed class Convention : BaseAuditableEntity<long>
    {
        public long NotificationId { get; private set; }
        public long ResourceId { get; private set; }
        public bool Enabled { get; private set; }

        public static Convention Create(long notificationId, long resourceId)
            => new Convention()
            {
                NotificationId = notificationId,
                ResourceId = resourceId,
                Enabled = true
            };

        public void Enable() => ChangeEnable(true);
        public void Disable() => ChangeEnable(false);

        private void ChangeEnable(bool enable)
        {
            Enabled = enable;
            UpdateModificationTime();
        }

        private void UpdateModificationTime() => ModificationTime = DateTime.Now;
    }
}
