using Microsoft.AspNetCore.Builder;

namespace NotifierApi.Grpc.Server.Extensions
{
    using Services;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseGrpcServices(this IApplicationBuilder app)
        {
            app.UseEndpoints(e =>
            {
                e.MapGrpcService<ServerGrpcService>();  
                e.MapGrpcReflectionService();                
            });

            return app;
        }
    }
}
