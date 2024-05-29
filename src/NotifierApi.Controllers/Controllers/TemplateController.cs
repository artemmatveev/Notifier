namespace NotifierApi.Controllers.Controllers
{
    using System.Threading;    
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddTemplate;
    using UseCase.Handlers.Command.DeleteTemplate;
    using UseCase.Handlers.Command.ChangeTemplateStatus;
    using UseCase.Handlers.Command.Transform;
    using UseCase.Handlers.Command.UpdateTemplate;
    using UseCase.Handlers.Command.UpdateTemplateContent;    
    using UseCase.Handlers.Query.FindTemplates;
    using UseCase.Handlers.Query.GetTemplate;
    using NotifierApi.Rest.V1.S0.Template;
    using NotifierApi.UseCase.Handlers.Query.GetContent;

    [Route("api/v1/templates")]
    [ApiController]
    public class TemplateController : BaseController<TemplateController>
    {
        readonly IMapper _mapper;

        public TemplateController(IMapper mapper, IMediator mediator)
            : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Get templates list
        /// </summary>
        /// <remarks>Get templates list</remarks>
        /// <response code="200">Returns notifications</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet()]
        [ProducesResponseType(typeof(OkResult<Collection<FindTemplatesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Collection<FindTemplatesResponse>>>> FindTemplatesAsync(
            [FromQuery] FindTemplatesRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindTemplatesRequest, FindTemplatesQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Collection<FindTemplatesResponse>>.Create(
                new Collection<FindTemplatesResponse>(_mapper.Map<IReadOnlyList<Domain.Template>, IReadOnlyList<FindTemplatesResponse>>(result))
            ));
        }

        /// <summary>
        ///     Get template by template id
        /// </summary>
        /// <remarks>Get template by template id</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OkResult<GetTemplateResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<GetTemplateResponse>>> GetTemplateAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, GetTemplateQuery>(id);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<GetTemplateResponse>.Create(
                _mapper.Map<Domain.Template, GetTemplateResponse>(result)));
        }

        /// <summary>
        ///     Get template content by template id
        /// </summary>
        /// <remarks>Get template content by template id</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}/content")]
        [ProducesResponseType(typeof(OkResult<GetTemplateResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<GetTemplateResponse>>> GetTemplateContentAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, GetTemplateContentQuery>(id);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<GetTemplateContentResponse>.Create(
                _mapper.Map<Domain.Template, GetTemplateContentResponse>(result)));
        }

        /// <summary>
        ///     Add a new template to the store
        /// </summary>
        /// <remarks>Add a new template to the store</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost()]
        [ProducesResponseType(typeof(OkResult<AddTemplateResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<AddTemplateResponse>>> AddTemplateAsync(
            [FromBody] AddTemplateRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddTemplateRequest, AddTemplateCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, OkResult<AddTemplateResponse>.Create(
                _mapper.Map<Domain.Template, AddTemplateResponse>(result)));
        }
        
        /// <summary>
        ///     Updates a template
        /// </summary>
        /// <remarks>Updates a template</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>       
        /// <response code="409">Conflict</response>       
        /// <response code="500">Internal Server Error</response>
        [HttpPatch()]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> UpdateTemplateAsync(
            [FromBody] UpdateTemplateRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateTemplateRequest, UpdateTemplateCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Change a template status
        /// </summary>
        /// <remarks>Change a template status</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("status")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> ChangeTemplateStatusAsync(
            [FromBody] ChangeTemplateStatusRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ChangeTemplateStatusRequest, ChangeTemplateStatusCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Updates a template content
        /// </summary>
        /// <remarks>Updates a template content</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>       
        /// <response code="409">Conflict</response>       
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("content")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> UpdateTemplateContentAsync(
            [FromBody] UpdateTemplateContentRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateTemplateContentRequest, UpdateTemplateContentCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Deletes a template
        /// </summary>
        /// <remarks>Deletes a template</remarks>
        /// <response code="200">Ok</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{templateId}")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> DeleteTemplateAsync(
            long templateId, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, DeleteTemplateCommand>(templateId);
            await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }
        
        /// <summary>
        ///     Transform data by template
        /// </summary>
        /// <returns></returns>
        [HttpPost("transform")]
        [ProducesResponseType(typeof(OkResult<TransformResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<TransformRequest>>> TransformAsync(
            [FromBody] TransformRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<TransformRequest, TransformCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<TransformResponse>.Create(null));
        }
    }
}
