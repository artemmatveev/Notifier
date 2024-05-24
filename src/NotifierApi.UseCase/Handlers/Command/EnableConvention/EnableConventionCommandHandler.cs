namespace NotifierApi.UseCase.Handlers.Command.EnableConvention
{
    internal sealed class EnableConventionCommandHandler : IRequestHandler<EnableConventionCommand, Unit>
    {
        readonly IConventionRepository _convnetionRepository;
        public EnableConventionCommandHandler(IConventionRepository convnetionRepository)
        {
            _convnetionRepository = convnetionRepository;
        }

        public async Task<Unit> Handle(EnableConventionCommand request, CancellationToken cancellationToken)
        {
            await _convnetionRepository.UpdateAsync(request.Id,

                request.Enabled is true ? e => e.Enable() : e => e.Disable());

            return Unit.Value;
        }
    }
}
