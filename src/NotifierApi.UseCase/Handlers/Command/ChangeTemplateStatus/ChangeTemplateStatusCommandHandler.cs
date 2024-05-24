using NotifierApi.UseCase.Handlers.Command.ChangeTemplateStatus;

namespace NotifierApi.UseCase.Handlers.Command.EnableTemplate
{
    sealed class ChangeTemplateStatusCommandHandler : IRequestHandler<ChangeTemplateStatusCommand, Unit>
    {
        readonly ITemplateRepository _templateRepository;
        public ChangeTemplateStatusCommandHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(ChangeTemplateStatusCommand request, CancellationToken cancellationToken)
        {
            await _templateRepository.UpdateAsync(request.Id, e => e.ChangeStatus(request.Status));

            return Unit.Value;
        }
    }
}
