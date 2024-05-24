namespace NotifierApi.UseCase.Handlers.Command.AddApplication
{
    internal sealed class AddApplicationCommandHandler : IRequestHandler<AddApplicationCommand, Application>
    {
        readonly IApplicationRepository _applicationRepository;
        public AddApplicationCommandHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = Application.Create(request.Name, request.Comment);

            return await _applicationRepository.AddAsync(application, e => e.Name != application.Name);
        }
    }
}
