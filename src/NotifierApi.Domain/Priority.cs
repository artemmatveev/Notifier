namespace NotifierApi.Domain
{
    public enum Priority : byte
    {
        Unspecified,
        Minor = 1,
        Lowest,
        Medium,
        High,
        Highest,
    }
}
