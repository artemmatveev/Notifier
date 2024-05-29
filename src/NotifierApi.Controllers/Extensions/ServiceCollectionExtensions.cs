namespace NotifierApi.Controllers.Extensions
{
    using Mappers;

    public static class ServiceCollectionExtensions
    {
        public static void AddRestControllers(this IServiceCollection services)
        {
            var assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name!);

            services.AddMvc()
                    .AddApplicationPart(assembly)
                    .AddControllersAsServices();

            services.AddAutoMapper(typeof(ControllerMapProfile));
        }
    }
}
