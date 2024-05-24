namespace NotifierApi.UseCase.Model
{
    public sealed record FindTelegramMessagesResult(
        IReadOnlyList<TelegramMessage> TelegramMessages, int TotalRecords);
}
