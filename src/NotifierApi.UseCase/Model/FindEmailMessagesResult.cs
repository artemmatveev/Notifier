namespace NotifierApi.UseCase.Model
{
    public sealed record FindEmailMessagesResult(
        IReadOnlyList<EmailMessage> EmailMessages, int TotalRecords);
}
