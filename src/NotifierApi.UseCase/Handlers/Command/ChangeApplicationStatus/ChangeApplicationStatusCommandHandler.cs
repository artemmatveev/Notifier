namespace NotifierApi.UseCase.Handlers.Command.ChangeApplicationStatus
{
    internal sealed class ChangeApplicationStatusCommandHandler : IRequestHandler<ChangeApplicationStatusCommand, Unit>
    {
        readonly IApplicationRepository _applicationRepository;
        public ChangeApplicationStatusCommandHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Unit> Handle(ChangeApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            await _applicationRepository.UpdateAsync(request.Id, e => e.ChangeStatus(request.Status));

            return Unit.Value;
        }
    }
}
