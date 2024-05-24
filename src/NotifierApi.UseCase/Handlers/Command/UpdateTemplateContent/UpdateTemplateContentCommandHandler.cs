namespace NotifierApi.UseCase.Handlers.Command.UpdateTemplateContent
{    
    internal sealed class UpdateTemplateContentCommandHandler : IRequestHandler<UpdateTemplateContentCommand, Unit>
    {
        readonly ITemplateRepository _templateRepository;
        public UpdateTemplateContentCommandHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(UpdateTemplateContentCommand request, CancellationToken cancellationToken)
        {
            await _templateRepository.UpdateAsync(request.Id, e => e.UpdateContent(request.Subject, request.Body));

            return Unit.Value;
        }
    }
}
