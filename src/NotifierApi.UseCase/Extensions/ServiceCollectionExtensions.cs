namespace NotifierApi.UseCase.Extensions
{    
    public static class ServiceCollectionExtensions
    {
        public static void AddUseCases(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());            
        }
    }
}
