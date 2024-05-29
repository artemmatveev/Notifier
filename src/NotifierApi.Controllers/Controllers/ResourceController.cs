using NotifierApi.Rest.V1.S0;
using NotifierApi.UseCase.Handlers.Command.UpdateResource;
using NotifierApi.UseCase.Handlers.Query.FindResources;
using NotifierApi.UseCase.Handlers.Query.GetResource;

namespace NotifierApi.Controllers.Controllers
{
    [Route("api/v1/resources")]
    [ApiController]
    public sealed class ResourceController : BaseController<ResourceController>
    {
        readonly IMapper _mapper;

        public ResourceController(IMapper mapper, IMediator mediator)
            : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Get resources list
        /// </summary>
        /// <remarks>Get resources list</remarks>
        /// <response code="200">Returns resources</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet()]
        [ProducesResponseType(typeof(OkResult<Collection<FindResourcesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Collection<FindResourcesResponse>>>> FindApplicationsAsync(
            [FromQuery] FindResourcesRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindResourcesRequest, FindResourcesQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Collection<FindResourcesResponse>>.Create(
                new Collection<FindResourcesResponse>(_mapper.Map<IReadOnlyList<Domain.Resource>, IReadOnlyList<FindResourcesResponse>>(result))));
        }

        /// <summary>
        ///     Get resource by resource id
        /// </summary>
        /// <remarks>Get resource by resource id</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OkResult<GetResourceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<GetResourceResponse>>> GetApplicationAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, GetResourceQuery>(id);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<GetResourceResponse>.Create(
                _mapper.Map<Domain.Resource, GetResourceResponse>(result)));
        }

        /// <summary>
        ///     Updates a resource
        /// </summary>
        /// <remarks>Updates a resource</remarks>
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
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> UpdateApplicationAsync(
            [FromBody] UpdateResourceRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateResourceRequest, UpdateResourceCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }
    }
}
