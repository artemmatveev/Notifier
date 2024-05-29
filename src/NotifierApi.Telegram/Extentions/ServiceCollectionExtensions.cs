namespace NotifierApi.Telegram.Extensions
{
    using ExternalServices;

    public static class ServiceCollectionExtensions
    {
        public static void AddTelegramSender(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient<ITelegramBotClient, TelegramBotClient>();
            services.AddScoped<ITelegramSenderService, TelegramSenderService>();
        }
    }
}
