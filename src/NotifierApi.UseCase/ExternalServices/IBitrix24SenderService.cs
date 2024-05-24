namespace NotifierApi.UseCase.ExternalServices
{
    using Domain;

    public interface IBitrix24SenderService
    {
        Task SendBitrix24Async(Bitrix24Message message);
    }
}
