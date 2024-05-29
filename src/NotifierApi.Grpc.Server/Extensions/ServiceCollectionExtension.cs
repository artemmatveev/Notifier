using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NotifierApi.Grpc.Server.Extensions
{
    using Mappers;

    public static class ServiceCollectionExtension
    {
        public static void AddGrpcServices(this IServiceCollection services, IConfiguration conf)
        {
            services.AddAutoMapper(typeof(GrpcServerMapProfile));
        }
    }
}
