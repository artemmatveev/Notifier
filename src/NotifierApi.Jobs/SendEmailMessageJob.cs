namespace NotifierApi.Jobs
{    
    [DisallowConcurrentExecution]
    internal class SendEmailMessageJob : IJob
    {
        readonly ILogger<SendEmailMessageJob> _logger;
        readonly IEmailSenderService _senderService;

        public SendEmailMessageJob(ILogger<SendEmailMessageJob> logger,
            IEmailSenderService senderService)
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
