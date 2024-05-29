namespace NotifierApi.RabbitMq.Options
{
    internal sealed record RabbitMqOptions(
        string Host,
        string VirtualHost,
        ushort Port,
        string Username,
        string Password)
    {
        public const string RabbitMq = nameof(RabbitMq);

        public RabbitMqOptions()
            : this(string.Empty, string.Empty, 0, string.Empty, string.Empty)
        { }
    }
}
