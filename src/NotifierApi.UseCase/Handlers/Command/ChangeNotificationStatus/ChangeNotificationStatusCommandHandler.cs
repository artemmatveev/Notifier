namespace NotifierApi.UseCase.Handlers.Command.ChangeNotificationStatus
{
    sealed class ChangeNotificationStatusCommandHandler : IRequestHandler<ChangeNotificationStatusCommand, Unit>
    {
        readonly INotificationRepository _notificationRepository;
        public ChangeNotificationStatusCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(ChangeNotificationStatusCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.UpdateAsync(request.Id, e => e.ChangeStatus(request.Status));

            return Unit.Value;
        }
    }
}
