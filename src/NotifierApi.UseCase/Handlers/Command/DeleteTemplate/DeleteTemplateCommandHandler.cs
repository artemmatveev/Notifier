namespace NotifierApi.UseCase.Handlers.Command.DeleteTemplate
{
    internal sealed class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, Unit>
    {
        readonly ITemplateRepository _templateRepository;
        public DeleteTemplateCommandHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            await _templateRepository.UpdateAsync(request.Id, e => e.Delete());

            return Unit.Value;
        }
    }
}
