namespace NotifierApi.UseCase.Services
{
    public class TelegramSenderService : ITelegramSenderService
    {
        readonly ITelegramMessageRepository _telegramMessageRepository;
        readonly ExternalServices.ITelegramSenderService _telegramSenderService;

        public TelegramSenderService(ITelegramMessageRepository telegramMessageRepository,
            ExternalServices.ITelegramSenderService telegramSenderService)
        {
            _telegramMessageRepository = telegramMessageRepository;
            _telegramSenderService = telegramSenderService;
        }

        public async Task SendMessagesAsync()
        {
            var messages = await _telegramMessageRepository.FindAllAsync(m => m.SentTime == null);

            foreach (var msg in messages)
            {
                await _telegramSenderService.SendTelegramAsync(msg);
                await _telegramMessageRepository.UpdateAsync(msg.Id, e => e.Send());
            }
        }
    }
}
