namespace NotifierApi.UseCase.Model
{
    public sealed record FindBitrix24MessagesResult(
       IReadOnlyList<Bitrix24Message> Bitrix24Messages, int TotalRecords);
}
