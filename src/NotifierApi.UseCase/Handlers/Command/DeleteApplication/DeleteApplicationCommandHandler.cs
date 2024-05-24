namespace NotifierApi.UseCase.Handlers.Command.DeleteApplication
{
    internal sealed class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, Unit>
    {
        readonly IApplicationRepository _applicationRepository;
        public DeleteApplicationCommandHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Unit> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            await _applicationRepository.UpdateAsync(request.Id, e => e.Delete());

            return Unit.Value;
        }
    }
}
