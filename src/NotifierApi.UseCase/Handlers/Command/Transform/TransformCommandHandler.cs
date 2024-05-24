namespace NotifierApi.UseCase.Handlers.Command.Transform
{
    internal sealed class TransformCommandHandler : IRequestHandler<TransformCommand, string>
    {
        readonly ITemplateRepository _templateRepository;
        public TransformCommandHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<string> Handle(TransformCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
