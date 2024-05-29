namespace NotifierApi.RabbitMq.Mappers
{
    using UseCase.Handlers.Command.AddMessage;

    internal sealed class RabbitMqMapProfiler : Profile
    {
        public RabbitMqMapProfiler()
        {
            // Message:
            CreateMap<SendNotification, AddMessageCommand>();
        }
    }
}
