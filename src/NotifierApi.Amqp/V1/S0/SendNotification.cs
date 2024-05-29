namespace NotifierApi.Amqp.V1.S0
{
    public sealed record SendNotification(
        Guid Constant,
        object Payload,
        string? FromName
    );
}
