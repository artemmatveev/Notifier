namespace NotifierApi.Domain.Tests
{
    internal static class Utils
    {
        private static Faker Faker = new Faker();

        public static Application GetApplicationByFaker(string? name = null)
            => Application.Create(
                    name: name ?? Faker.Random.String2(1, 150),
                    comment: Faker.Lorem.Sentence());

        public static Channel GetChannelByFaker(string? name = null, string? data = null)
             => Channel.Create(
                    name: name ?? Faker.Random.String2(1, 150),
                    data: data ?? "{}",
                    transport: Transport.Email);

        public static Convention GetConventionByFaker()
            => Convention.Create(
                    resourceId: Faker.Random.Long(1),
                    notificationId: Faker.Random.Long(1));

        public static EmailMessage GetEmailMessageByFaker(string? fromName = null, string? fromEmail = null,
                        string? toName = null, string? toEmail = null, string? subject = null, string? body = null)
            => EmailMessage.Create(
                    resourceId: Faker.Random.Long(1),
                    notificationId: Faker.Random.Long(1),
                    fromName: fromName ?? Faker.Random.String2(1, 255),
                    fromEmail: fromEmail ?? Faker.Internet.Email(),
                    toName: toName ?? Faker.Random.String2(1, 255),
                    toEmail: toEmail ?? Faker.Internet.Email(),
                    subject: subject ?? Faker.Random.String2(1, 150),
                    body: body ?? Faker.Lorem.Sentence(),
                    priority: Faker.PickRandom<Priority>());

        public static Notification GetNotificationByFaker(string? name = null)
            => Notification.Create(
                    application: GetApplicationByFaker(),
                    priority: Priority.Minor,
                    name: name ?? Faker.Random.String2(1, 150),
                    comment: Faker.Lorem.Sentence(),
                    channels: new List<Channel>() {
                        GetChannelByFaker(),
                        GetChannelByFaker()
                    });

        public static Resource GetResourceByFaker(string? name = null, string? email = null, string? username = null, long? chatId = null)
            => Resource.Create(
                    staffId: Faker.Random.Int(1),
                    name: name ?? Faker.Random.String2(1, 255),
                    email: email ?? Faker.Internet.Email(),
                    username: username ?? Faker.Internet.UserName(),
                    chatId: chatId ?? Faker.Random.Long(1));

        public static TelegramMessage GetTelegramMessageByFaker(long? chatId = null)
            => TelegramMessage.Create(
                    notificationId: Faker.Random.Long(1),
                    resourceId: Faker.Random.Long(1),
                    body: Faker.Lorem.Sentence(),
                    priority: Faker.PickRandom<Priority>(),
                    chatId: chatId ?? Faker.Random.Long(1));

        public static Template GetTemplateByFaker(string? name = null, string? subject = null, string? body = null, string? comment = null)
            => Template.Create(
                    notification: GetNotificationByFaker(),
                    transport: Transport.Email,
                    lang: Lang.Ru,
                    subject: subject ?? Faker.Random.String2(1, 150),
                    body: body ?? Faker.Lorem.Sentence(),
                    name: name ?? Faker.Random.String2(1, 150),
                    comment: comment ?? Faker.Lorem.Sentence());

        public static string RandomString(this int maxLenght)
            => Faker.Random.String2(1, maxLenght);

    }
}
