namespace NotifierApi.Domain
{
    public enum Status : byte
    {
        Unspecified,
        Enabled = 1,
        Disabled,
        Deleted
    }
}
