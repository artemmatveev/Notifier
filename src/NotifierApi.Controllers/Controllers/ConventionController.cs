namespace NotifierApi.Controllers.Controllers
{    
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddConvention;
    using UseCase.Handlers.Command.EnableConvention;
    using UseCase.Handlers.Query.FindConventions;

    [Route("api/v1/conventions")]
    [ApiController]
    public class ConventionController : BaseController<ConventionController>
    {
        readonly IMapper _mapper;

        public ConventionController(IMapper mapper, IMediator mediator)
            : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Get conventions list
        /// </summary>
        /// <remarks>Get conventions list</remarks>
        /// <response code="200">Returns conventions</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet()]
        [ProducesResponseType(typeof(OkResult<Collection<FindConventionsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Collection<FindConventionsResponse>>>> FindConventionsAsync(
            [FromQuery] FindConventionsRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindConventionsRequest, FindConventionsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Collection<FindConventionsResponse>>.Create(
                new Collection<FindConventionsResponse>(_mapper.Map<IReadOnlyList<Domain.Convention>, IReadOnlyList<FindConventionsResponse>>(result))));
        }

        /// <summary>
        ///     Add a new convention to the store
        /// </summary>
        /// <remarks>Add a new convention to the store</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost()]
        [ProducesResponseType(typeof(OkResult<AddConventionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<AddConventionResponse>>> AddConventionAsync(
            [FromBody] AddConventionRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddConventionRequest, AddConventionCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, OkResult<AddConventionResponse>.Create(
                _mapper.Map<Domain.Convention, AddConventionResponse>(result)));
        }


        /// <summary>
        ///     Enable a convention
        /// </summary>
        /// <remarks>Enable a convention</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>       
        /// <response code="409">Conflict</response>       
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("enable")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> EnableConventionAsync(
            [FromBody] EnableConventionRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<EnableConventionRequest, EnableConventionCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }
    }
}
