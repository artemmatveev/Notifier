namespace NotifierApi.RabbitMq.Consumers
{
    using UseCase.Handlers.Command.AddMessage;

    [AsyncApi]
    internal sealed class SendNotificationConsumer : IConsumer<SendNotification>
    {
        readonly IMapper _mapper;
        readonly IMediator _mediator;

        public SendNotificationConsumer(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [Channel("notifierApi.amqp.v1.s0:sendNotification")]
        [SubscribeOperation(typeof(SendNotification),
                Summary = $"Принимает сообщение {nameof(SendNotification)}",
                OperationId = $"{nameof(SendNotification)}Consum",
                Description = $"Обработка сообщения {nameof(SendNotification)}")]
        public async Task Consume(ConsumeContext<SendNotification> context)
        {
            var message = context.Message;

            var command = _mapper.Map<SendNotification, AddMessageCommand>(message);
            await _mediator.Send(command);
        }
    }
}
