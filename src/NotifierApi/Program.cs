using Iems.Framework.Grpc.Extentions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Настройка параметров веб-сервера Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    int httpPort = 3024; //int.Parse(Environment.GetEnvironmentVariable("HTTP_PORT")!);
    options.Listen(IPAddress.Any, httpPort, listenOptions => listenOptions.Protocols = HttpProtocols.Http1);

    int grpcPort = 3025; //int.Parse(Environment.GetEnvironmentVariable("GRPC_PORT")!);
    options.Listen(IPAddress.Any, grpcPort, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
});

var configuration = builder.Configuration;
var environment = builder.Environment;

#region logging
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo
    .File(
        path: $@"{configuration["Logging:Path"]}\{configuration["DefaultAppName"]}.txt",
        fileSizeLimitBytes: 1_000_000,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1))
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region health
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health");
    opts.SetEvaluationTimeInSeconds(5);
    opts.SetMinimumSecondsBetweenFailureNotifications(10);
}).AddInMemoryStorage();
#endregion

builder.Services.AddOptions();
builder.Services.AddJsonOptions(configuration);
builder.Services.AddRestPresentation(configuration, environment);
builder.Services.AddUseCases(configuration);
builder.Services.AddEmailSender(configuration);
builder.Services.AddTelegramSender(configuration);
builder.Services.AddBitrix24Sender(configuration);
builder.Services.AddDataProvider(configuration);
builder.Services.AddGrpcInterceptors(configuration);
builder.Services.AddGrpcServices(configuration);
builder.Services.AddAmqpConsumers(configuration, environment);
builder.Services.AddRestControllers();
builder.Services.AddJobs();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

#region metrics
app.MapMetrics();
app.UseHttpMetrics();
#endregion

#region health
app.UseHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/liveness", new HealthCheckOptions { });
app.UseHealthChecksUI(setup =>
{
    setup.UIPath = "/health-ui";
    setup.ApiPath = "/health-ui-api";
});
app.MapGet("/", context =>
{
    context.Response.Redirect("/health-ui");
    return Task.FromResult(0);
});
#endregion

app.UseRestPresentation(configuration, environment);
app.UseRestErrorMiddleware();
app.UseGrpcServices();
app.UseAsyncPresentation(configuration, environment);

app.Run();
