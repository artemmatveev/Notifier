namespace NotifierApi.Jobs
{    
    [DisallowConcurrentExecution]
    internal class SendBitrix24MessageJob : IJob
    {
        readonly ILogger<SendBitrix24MessageJob> _logger;
        readonly IBitrix24SenderService _senderService;

        public SendBitrix24MessageJob(ILogger<SendBitrix24MessageJob> logger,
            IBitrix24SenderService senderService)
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
