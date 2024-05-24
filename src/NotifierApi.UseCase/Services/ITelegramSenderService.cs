namespace NotifierApi.UseCase.Services
{
    public interface ITelegramSenderService
    {
        Task SendMessagesAsync();
    }
}
