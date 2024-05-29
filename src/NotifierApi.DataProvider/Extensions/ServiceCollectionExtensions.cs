namespace NotifierApi.DataProvider.Extensions
{
    using Context;
    using Options;
    using Repository;

    public static class ServiceCollectionExtensions
    {
        public static void AddDataProvider(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IEmailMessageRepository, EmailMessageRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IConventionRepository, ConventionRepository>();
            services.AddScoped<ITelegramMessageRepository, TelegramMessageRepository>();
            services.AddScoped<IBitrix24MessageRepository, Bitrix24MessageRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();

            var options = configuration
                    .GetSection(SqlServerOptions.SqlServer)
                    .Get<SqlServerOptions>();

            services.AddHealthChecks()
                .AddSqlServer(Crypto.DecryptString(
                    options.EncryptedConnectionString, SqlServerOptions.Key),
                            "SELECT TOP (1) [id] FROM [dbo].[application]",
                            null,
                            "notifier-db",
                            HealthStatus.Healthy,
                            new string[] { "ready", "mssql" });


            services.AddDbContext<NotifierDbContext>(ConfigureContext(options.EncryptedConnectionString));
        }

#if DEBUG
        public static readonly ILoggerFactory loggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
#endif

        private static Action<DbContextOptionsBuilder> ConfigureContext(string encryptedConnectionString)
            => builder =>
            {
                ArgumentNullException.ThrowIfNull(encryptedConnectionString);
                builder.UseSqlServer(Crypto.DecryptString(encryptedConnectionString, SqlServerOptions.Key));

#if DEBUG
                builder.UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
#endif
            };
    }
}
