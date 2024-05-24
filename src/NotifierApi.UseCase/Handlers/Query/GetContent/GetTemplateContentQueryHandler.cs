using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotifierApi.UseCase.Handlers.Query.GetTemplate;

namespace NotifierApi.UseCase.Handlers.Query.GetContent
{
    internal sealed class GetTemplateContentQueryHandler : IRequestHandler<GetTemplateContentQuery, Template>
    {
        ITemplateRepository _templateRepository;

        public GetTemplateContentQueryHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<Template> Handle(GetTemplateContentQuery request, CancellationToken cancellationToken)
            => await _templateRepository.GetAsync(e => e.Id == request.Id);
    }
}
