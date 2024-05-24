namespace NotifierApi.UseCase.Handlers.Query.GetNotification
{
    internal sealed class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, Notification>
    {
        readonly INotificationRepository _notificationRepository;

        public GetNotificationQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
            => await _notificationRepository.GetAsync(e => e.Id == request.Id);
    }
}
