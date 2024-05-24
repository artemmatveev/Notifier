namespace NotifierApi.Domain
{
    public enum Transport : byte
    {
        Unspecified,
        Email = 1,
        Telegram,
        Push,
        Bitrix24
    }
}
