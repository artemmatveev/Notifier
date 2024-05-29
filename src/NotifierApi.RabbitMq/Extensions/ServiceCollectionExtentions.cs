namespace NotifierApi.RabbitMq.Extensions
{
    using Consumers;
    using Mappers;
    using MassTransit;
    using Options;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAmqpConsumers(this IServiceCollection services,
           IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddAsyncPresentation(configuration, env, new[] {
                typeof(SendNotificationConsumer)
            });

            services.AddAutoMapper(typeof(RabbitMqMapProfiler));

            var options = configuration
                .GetSection(RabbitMqOptions.RabbitMq)
                .Get<RabbitMqOptions>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<SendNotificationConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(options.Host, options.Port, options.VirtualHost, h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                        h.Heartbeat(TimeSpan.FromSeconds(10));
                        h.RequestedConnectionTimeout(TimeSpan.FromSeconds(10));
                    });
                    cfg.MessageTopology.SetEntityNameFormatter(CamelCaseNameFormatter.Instance);
                    cfg.ReceiveEndpoint("notifierApi.sendNotificationComsumer", e =>
                    {
                        e.PrefetchCount = 50;
                        e.ConfigureConsumer<SendNotificationConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
