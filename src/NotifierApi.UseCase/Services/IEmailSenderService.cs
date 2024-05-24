namespace NotifierApi.UseCase.Services
{
    public interface IEmailSenderService
    {
        Task SendMessagesAsync();
    }
}
