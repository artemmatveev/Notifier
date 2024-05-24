namespace NotifierApi.UseCase.Options
{
    public sealed record EmailOptions(string FromEmail, string FromName)
    {
        public const string Email = nameof(Email);

        public EmailOptions()
            : this(string.Empty, string.Empty)
        { }
    }
}