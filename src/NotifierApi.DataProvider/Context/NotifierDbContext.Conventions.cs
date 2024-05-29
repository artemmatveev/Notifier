namespace NotifierApi.DataProvider.Context
{
    internal sealed partial class NotifierDbContext
    {
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Application>(e =>
            {
                e.ToTable(nameof(Application).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).HasColumnName(nameof(Application.Name).ToSnakeCase()).HasMaxLength(150);
                e.Property(e => e.Comment).HasColumnName(nameof(Application.Comment).ToSnakeCase());                
                e.Property(e => e.Status).HasColumnName(nameof(Application.Status).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(Application.CreationTime).ToSnakeCase());
                e.Property(e => e.ModificationTime).HasColumnName(nameof(Application.ModificationTime).ToSnakeCase());
            });

            mb.Entity<Channel>(e =>
            {
                e.ToTable(nameof(Channel).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).HasColumnName(nameof(Channel.Name).ToSnakeCase()).HasMaxLength(150);
                e.Property(e => e.Transport).HasColumnName(nameof(Channel.Transport).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.Data).HasColumnName(nameof(Channel.Data).ToSnakeCase());                
                e.Property(e => e.Status).HasColumnName(nameof(Channel.Status).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(Channel.CreationTime).ToSnakeCase());
                e.Property(e => e.ModificationTime).HasColumnName(nameof(Channel.ModificationTime).ToSnakeCase());
            });

            mb.Entity<Convention>(e =>
            {
                e.ToTable(nameof(Convention).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.NotificationId).HasColumnName(nameof(Convention.NotificationId).ToSnakeCase());
                e.Property(e => e.ResourceId).HasColumnName(nameof(Convention.ResourceId).ToSnakeCase());
                e.Property(e => e.Enabled).HasColumnName(nameof(Convention.Enabled).ToSnakeCase());
                e.Property(e => e.CreationTime).HasColumnName(nameof(Convention.CreationTime).ToSnakeCase());
                e.Property(e => e.ModificationTime).HasColumnName(nameof(Convention.ModificationTime).ToSnakeCase());
            });

            mb.Entity<Notification>(e =>
            {
                e.ToTable(nameof(Notification).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.ApplicationId).HasColumnName(nameof(Notification.ApplicationId).ToSnakeCase());
                e.Property(e => e.Constant).HasColumnName(nameof(Notification.Constant).ToSnakeCase());
                e.Property(e => e.Priority).HasColumnName(nameof(Notification.Priority).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.Name).HasColumnName(nameof(Notification.Name).ToSnakeCase());
                e.Property(e => e.Comment).HasColumnName(nameof(Notification.Comment).ToSnakeCase());                
                e.Property(e => e.Status).HasColumnName(nameof(Notification.Status).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(Notification.CreationTime).ToSnakeCase());
                e.Property(e => e.ModificationTime).HasColumnName(nameof(Notification.ModificationTime).ToSnakeCase());
            });


            mb.Entity<NotificationChannel>(e =>
            {
                e.ToTable(nameof(NotificationChannel).ToSnakeCase());
                e.HasKey(e => new { e.NotificationId, e.ChannelId });
                e.Property(e => e.NotificationId).HasColumnName(nameof(NotificationChannel.NotificationId).ToSnakeCase());
                e.Property(e => e.ChannelId).HasColumnName(nameof(NotificationChannel.ChannelId).ToSnakeCase());

                e.HasOne(e => e.Notification).WithMany(n => n.NotificationChannels).HasForeignKey(e => e.NotificationId);
                e.HasOne(e => e.Channel).WithMany(ch => ch.NotificationChannels).HasForeignKey(e => e.ChannelId);
            });


            mb.Entity<Template>(e =>
            {
                e.ToTable(nameof(Template).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.NotificationId).HasColumnName(nameof(Template.NotificationId).ToSnakeCase());
                e.Property(e => e.Transport).HasColumnName(nameof(Template.Transport).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.Lang).HasColumnName(nameof(Template.Lang).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.Subject).HasColumnName(nameof(Template.Subject).ToSnakeCase());
                e.Property(e => e.Body).HasColumnName(nameof(Template.Body).ToSnakeCase());
                e.Property(e => e.Name).HasColumnName(nameof(Template.Name).ToSnakeCase());
                e.Property(e => e.Comment).HasColumnName(nameof(Template.Comment).ToSnakeCase());                
                e.Property(e => e.Status).HasColumnName(nameof(Template.Status).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(Template.CreationTime).ToSnakeCase());
                e.Property(e => e.ModificationTime).HasColumnName(nameof(Template.ModificationTime).ToSnakeCase());
            });

            mb.Entity<Resource>(e =>
            {
                e.ToTable(nameof(Resource).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.StaffId).HasColumnName(nameof(Resource.StaffId).ToSnakeCase());
                e.Property(e => e.Name).HasColumnName(nameof(Resource.Name).ToSnakeCase());
                e.Property(e => e.Email).HasColumnName(nameof(Resource.Email).ToSnakeCase());
                e.Property(e => e.Username).HasColumnName(nameof(Resource.Username).ToSnakeCase());
                e.Property(e => e.ChatId).HasColumnName(nameof(Resource.ChatId).ToSnakeCase());
                e.Property(e => e.Bitrix24UserId).HasColumnName(nameof(Resource.Bitrix24UserId).ToSnakeCase());
                e.Property(e => e.CreationTime).HasColumnName(nameof(Resource.CreationTime).ToSnakeCase());
                e.Property(e => e.ModificationTime).HasColumnName(nameof(Resource.ModificationTime).ToSnakeCase());
            });

            mb.Entity<EmailMessage>(e =>
            {
                e.ToTable(nameof(EmailMessage).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.NotificationId).HasColumnName(nameof(EmailMessage.NotificationId).ToSnakeCase());
                e.Property(e => e.ResourceId).HasColumnName(nameof(EmailMessage.ResourceId).ToSnakeCase());
                e.Property(e => e.FromName).HasColumnName(nameof(EmailMessage.FromName).ToSnakeCase());
                e.Property(e => e.FromEmail).HasColumnName(nameof(EmailMessage.FromEmail).ToSnakeCase());
                e.Property(e => e.ToName).HasColumnName(nameof(EmailMessage.ToName).ToSnakeCase());
                e.Property(e => e.ToEmail).HasColumnName(nameof(EmailMessage.ToEmail).ToSnakeCase());
                e.Property(e => e.Subject).HasColumnName(nameof(EmailMessage.Subject).ToSnakeCase());
                e.Property(e => e.Body).HasColumnName(nameof(EmailMessage.Body).ToSnakeCase()).IsRequired();
                e.Property(e => e.Priority).HasColumnName(nameof(EmailMessage.Priority).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(EmailMessage.CreationTime).ToSnakeCase());
                e.Property(e => e.SentTime).HasColumnName(nameof(EmailMessage.SentTime).ToSnakeCase());
                e.Property(e => e.ReceiveTime).HasColumnName(nameof(EmailMessage.ReceiveTime).ToSnakeCase());
            });

            mb.Entity<TelegramMessage>(e =>
            {
                e.ToTable(nameof(TelegramMessage).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.NotificationId).HasColumnName(nameof(TelegramMessage.NotificationId).ToSnakeCase());
                e.Property(e => e.ResourceId).HasColumnName(nameof(TelegramMessage.ResourceId).ToSnakeCase());
                e.Property(e => e.ChatId).HasColumnName(nameof(TelegramMessage.ChatId).ToSnakeCase());
                e.Property(e => e.Body).HasColumnName(nameof(TelegramMessage.Body).ToSnakeCase()).IsRequired();
                e.Property(e => e.Priority).HasColumnName(nameof(TelegramMessage.Priority).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(TelegramMessage.CreationTime).ToSnakeCase());
                e.Property(e => e.SentTime).HasColumnName(nameof(TelegramMessage.SentTime).ToSnakeCase());
                e.Property(e => e.ReceiveTime).HasColumnName(nameof(TelegramMessage.ReceiveTime).ToSnakeCase());
            });

            mb.Entity<Bitrix24Message>(e =>
            {
                e.ToTable(nameof(Bitrix24Message).ToSnakeCase());
                e.HasKey(e => e.Id);
                e.Property(e => e.NotificationId).HasColumnName(nameof(Bitrix24Message.NotificationId).ToSnakeCase());
                e.Property(e => e.ResourceId).HasColumnName(nameof(Bitrix24Message.ResourceId).ToSnakeCase());
                e.Property(e => e.UserId).HasColumnName(nameof(Bitrix24Message.UserId).ToSnakeCase());
                e.Property(e => e.Body).HasColumnName(nameof(Bitrix24Message.Body).ToSnakeCase()).IsRequired();
                e.Property(e => e.Priority).HasColumnName(nameof(Bitrix24Message.Priority).ToSnakeCase(true)).HasConversion<int>();
                e.Property(e => e.CreationTime).HasColumnName(nameof(Bitrix24Message.CreationTime).ToSnakeCase());
                e.Property(e => e.SentTime).HasColumnName(nameof(Bitrix24Message.SentTime).ToSnakeCase());
                e.Property(e => e.ReceiveTime).HasColumnName(nameof(Bitrix24Message.ReceiveTime).ToSnakeCase());
            });

            base.OnModelCreating(mb);
        }
    }
}
