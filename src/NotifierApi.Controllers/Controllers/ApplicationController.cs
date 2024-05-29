namespace NotifierApi.Controllers.Controllers
{
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddApplication;
    using UseCase.Handlers.Command.DeleteApplication;
    using UseCase.Handlers.Command.ChangeApplicationStatus;
    using UseCase.Handlers.Command.UpdateApplication;
    using UseCase.Handlers.Query.FindApplications;
    using UseCase.Handlers.Query.GetApplication;

    [Route("api/v1/applications")]
    [ApiController]
    public sealed class ApplicationController : BaseController<ApplicationController>
    {
        readonly IMapper _mapper;

        public ApplicationController(IMapper mapper, IMediator mediator)
            : base(mediator)
        {
            _mapper = mapper;
        }


        /// <summary>
        ///     Get applications list
        /// </summary>
        /// <remarks>Get applications list</remarks>
        /// <response code="200">Returns applications</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet()]
        [ProducesResponseType(typeof(OkResult<Collection<FindApplicationsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Collection<FindApplicationsResponse>>>> FindApplicationsAsync(
            [FromQuery] FindApplicationsRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindApplicationsRequest, FindApplicationsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Collection<FindApplicationsResponse>>.Create(
                new Collection<FindApplicationsResponse>(_mapper.Map<IReadOnlyList<Domain.Application>, IReadOnlyList<FindApplicationsResponse>>(result))));
        }


        /// <summary>
        ///     Add a new application to the store
        /// </summary>
        /// <remarks>Add a new application to the store</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost()]
        [ProducesResponseType(typeof(OkResult<AddApplicationResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<AddApplicationResponse>>> AddApplicationAsync(
            [FromBody] AddApplicationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddApplicationRequest, AddApplicationCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, OkResult<AddApplicationResponse>.Create(
                _mapper.Map<Domain.Application, AddApplicationResponse>(result)));
        }


        /// <summary>
        ///     Get application by application id
        /// </summary>
        /// <remarks>Get application by application id</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OkResult<GetApplicationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<GetApplicationResponse>>> GetApplicationAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, GetApplicationQuery>(id);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<GetApplicationResponse>.Create(
                _mapper.Map<Domain.Application, GetApplicationResponse>(result)));
        }


        /// <summary>
        ///     Updates a application
        /// </summary>
        /// <remarks>Updates a application</remarks>
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
            [FromBody] UpdateApplicationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateApplicationRequest, UpdateApplicationCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Change a application status
        /// </summary>
        /// <remarks>Change a application status</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("status")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> ChangeApplicationStatusAsync(
            [FromBody] ChangeApplicationStatusRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ChangeApplicationStatusRequest, ChangeApplicationStatusCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }


        /// <summary>
        ///     Deletes a application
        /// </summary>
        /// <remarks>Deletes a application</remarks>
        /// <response code="200">Ok</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> DeleteApplicationAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, DeleteApplicationCommand>(id);
            await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }
    }
}
