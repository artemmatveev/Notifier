namespace NotifierApi.Bitrix24.Extensions
{
    using ExternalServices;    

    public static class ServiceCollectionExtensions
    {
        public static void AddBitrix24Sender(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IBitrix24SenderService, Bitrix24SenderService>();
        }
    }
}
