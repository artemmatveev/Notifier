namespace NotifierApi.Email.Extensions
{
    using ExternalServices;

    public static class ServiceCollectionExtensions
    {
        public static void AddEmailSender(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
