namespace NotifierApi.UseCase.ExternalServices
{
    public interface ITelegramSenderService
    {
        Task SendTelegramAsync(TelegramMessage message);
    }
}
