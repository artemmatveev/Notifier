namespace NotifierApi.UseCase.Handlers.Command.UpdateTemplate
{
    internal sealed class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, Unit>
    {
        readonly ITemplateRepository _templateRepository;
        public UpdateTemplateCommandHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Unit> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            await _templateRepository.UpdateAsync(request.Id,

                 e => e.Update(request.Transport, request.Lang, request.Name, request.Comment),

                 e => e.Name != request.Name || (e.Name == request.Name && e.Id == request.Id));

            return Unit.Value;
        }
    }
}
