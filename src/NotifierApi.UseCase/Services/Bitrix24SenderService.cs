namespace NotifierApi.UseCase.Services
{
    public class Bitrix24SenderService : IBitrix24SenderService
    {
        readonly IBitrix24MessageRepository _bitrix24MessageRepository;
        readonly ExternalServices.IBitrix24SenderService _bitrix24SenderService;

        public Bitrix24SenderService(IBitrix24MessageRepository bitrix24MessageRepository,
            ExternalServices.IBitrix24SenderService bitrix24SenderService)
        {
            _bitrix24MessageRepository = bitrix24MessageRepository;
            _bitrix24SenderService = bitrix24SenderService;
        }

        public async Task SendMessagesAsync()
        {
            var messages = await _bitrix24MessageRepository.FindAllAsync(m => m.SentTime == null);

            foreach (var msg in messages)
            {
                await _bitrix24SenderService.SendBitrix24Async(msg);
                await _bitrix24MessageRepository.UpdateAsync(msg.Id, e => e.Send());
            }
        }
    }
}
