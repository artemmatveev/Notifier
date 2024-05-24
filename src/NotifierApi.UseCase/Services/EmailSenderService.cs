namespace NotifierApi.UseCase.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        readonly IEmailMessageRepository _emailMessageRepository;
        readonly ExternalServices.IEmailSenderService _emailSenderService;

        public EmailSenderService(IEmailMessageRepository emailMessageRepository,
            ExternalServices.IEmailSenderService emailSenderService)
        {
            _emailMessageRepository = emailMessageRepository;
            _emailSenderService = emailSenderService;
        }

        public async Task SendMessagesAsync()
        {
            var messages = await _emailMessageRepository.FindAllAsync(m => m.SentTime == null);

            foreach (var msg in messages)
            {
                await _emailSenderService.SendEmailAsync(msg);
                await _emailMessageRepository.UpdateAsync(msg.Id, e => e.Send());
            }
        }
    }
}
