namespace NotifierApi.UseCase.Handlers.Query.FindNotifications
{
    internal sealed class FindNotificationsQueryHandler : IRequestHandler<FindNotificationsQuery, IReadOnlyList<Notification>>
    {
        readonly INotificationRepository _notificationRepository;
        public FindNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IReadOnlyList<Notification>> Handle(FindNotificationsQuery query, CancellationToken cancellationToken)
            => await _notificationRepository.FindAllAsync(query.GetExpression(), nameof(Notification.Name), SortOrder.Asc);
    }
}
