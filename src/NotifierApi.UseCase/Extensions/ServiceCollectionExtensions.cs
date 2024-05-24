namespace NotifierApi.UseCase.Extensions
{
    using Services;
    using UseCase.Options;

    public static class ServiceCollectionExtensions
    {
        public static void AddUseCases(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ITelegramSenderService, TelegramSenderService>();
            services.AddScoped<IBitrix24SenderService, Bitrix24SenderService>();
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.Email));
        }
    }
}
