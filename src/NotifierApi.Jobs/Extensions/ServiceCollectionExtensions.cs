using Microsoft.Extensions.DependencyInjection;

namespace NotifierApi.Jobs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });

                q.ScheduleJob<SendEmailMessageJob>(trigger => trigger
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x =>
                        x.WithIntervalInSeconds(5).RepeatForever())
                );

                q.ScheduleJob<SendTelegramMessageJob>(trigger => trigger
                   .WithIdentity("trigger2", "group2")
                   .StartNow()
                   .WithSimpleSchedule(x =>
                       x.WithIntervalInSeconds(5).RepeatForever())
               );

                q.ScheduleJob<SendBitrix24MessageJob>(trigger => trigger
                   .WithIdentity("trigger3", "group3")
                   .StartNow()
                   .WithSimpleSchedule(x =>
                       x.WithIntervalInSeconds(5).RepeatForever())
               );
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.AddScoped<SendEmailMessageJob>();
            services.AddScoped<SendTelegramMessageJob>();
            services.AddScoped<SendBitrix24MessageJob>();
        }
    }
}
