namespace NotifierApi.UseCase.Handlers.Command.DeleteNotification
{
    internal sealed class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, Unit>
    {
        readonly INotificationRepository _notificationRepository;
        public DeleteNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.UpdateAsync(request.Id, e => e.Delete());

            return Unit.Value;
        }
    }
}
