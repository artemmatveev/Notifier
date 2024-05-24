namespace NotifierApi.UseCase.Handlers.Command.UpdateResource
{
    internal sealed class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand, Unit>
    {
        readonly IResourceRepository _resourceRepository;
        public UpdateResourceCommandHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            await _resourceRepository.UpdateAsync(request.Id,

                e => e.Update(request.ChatId),

                e => e.ChatId != request.ChatId || (e.ChatId == request.ChatId && e.Id == request.Id) || e.ChatId == null);

            return Unit.Value;
        }
    }
}
