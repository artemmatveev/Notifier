namespace NotifierApi.UseCase.Handlers.Command.UpdateApplication
{
    internal sealed class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, Unit>
    {
        readonly IApplicationRepository _applicationRepository;
        public UpdateApplicationCommandHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Unit> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            await _applicationRepository.UpdateAsync(request.Id,

                e => e.Update(request.Name, request.Comment),

                e => e.Name != request.Name || (e.Name == request.Name && e.Id == request.Id));

            return Unit.Value;
        }
    }
}
