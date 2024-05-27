namespace NotifierApi.Email.Options
{
    public sealed record SmtpOptions(
        int Port,
        string Host,
        SmtpDeliveryMethod DeliveryMethod,
        bool UseDefaultCredentials,
        string CredentialsUserName,
        string CredentialsPassword,
        string FromEmail,
        string FromName,
        bool EnableSsl);
}
