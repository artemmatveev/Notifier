namespace NotifierApi.UseCase.ExternalServices
{
    using Domain;

    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
