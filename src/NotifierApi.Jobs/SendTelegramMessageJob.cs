namespace NotifierApi.Jobs
{    
    [DisallowConcurrentExecution]
    internal class SendTelegramMessageJob : IJob
    {
        readonly ILogger<SendTelegramMessageJob> _logger;
        readonly ITelegramSenderService _senderService;

        public SendTelegramMessageJob(ILogger<SendTelegramMessageJob> logger,
            ITelegramSenderService senderService)
        {
            _logger = logger;
            _senderService = senderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _senderService.SendMessagesAsync();
        }
    }
}
