namespace NotifierApi.Controllers.Controllers
{
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddChannel;
    using UseCase.Handlers.Command.DeleteChannel;
    using UseCase.Handlers.Command.ChangeChannelStatus;
    using UseCase.Handlers.Command.UpdateChannel;
    using UseCase.Handlers.Query.FindChannels;
    using UseCase.Handlers.Query.GetChannel;

    [Route("api/v1/channels")]
    [ApiController]
    public sealed class ChannelController : BaseController<ChannelController>
    {
        readonly IMapper _mapper;

        public ChannelController(IMapper mapper, IMediator mediator)
           : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Get channels list
        /// </summary>
        /// <remarks>Get channels list</remarks>
        /// <response code="200">Returns channels</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet()]
        [ProducesResponseType(typeof(OkResult<Collection<FindChannelsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Collection<FindChannelsResponse>>>> FindChannelsAsync(
            [FromQuery] FindChannelsRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindChannelsRequest, FindChannelsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Collection<FindChannelsResponse>>.Create(
                new Collection<FindChannelsResponse>(_mapper.Map<IReadOnlyList<Domain.Channel>, IReadOnlyList<FindChannelsResponse>>(result))));
        }


        /// <summary>
        ///     Add a new channel to the store
        /// </summary>
        /// <remarks>Add a new channel to the store</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost()]
        [ProducesResponseType(typeof(OkResult<AddChannelResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<AddChannelResponse>>> AddChannelAsync(
            [FromBody] AddChannelRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddChannelRequest, AddChannelCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, OkResult<AddChannelResponse>.Create(
                _mapper.Map<Domain.Channel, AddChannelResponse>(result)));
        }


        /// <summary>
        ///     Get channel by channel id
        /// </summary>
        /// <remarks>Get channel by channel id</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OkResult<GetChannelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<GetChannelResponse>>> GetChannelAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, GetChannelQuery>(id);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<GetChannelResponse>.Create(
                _mapper.Map<Domain.Channel, GetChannelResponse>(result)));
        }


        /// <summary>
        ///     Update a channel
        /// </summary>
        /// <remarks>Update a channel</remarks>
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
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> UpdateChannelAsync(
            [FromBody] UpdateChannelRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateChannelRequest, UpdateChannelCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Change a channel status
        /// </summary>
        /// <remarks>Change a channel status</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("status")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> ChangeChannelStatusAsync(
            [FromBody] ChangeChannelStatusRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ChangeChannelStatusRequest, ChangeChannelStatusCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }


        /// <summary>
        ///     Deletes a channel
        /// </summary>
        /// <remarks>Deletes a channel</remarks>
        /// <response code="200">Ok</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> DeleteChannelAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, DeleteChannelCommand>(id);
            await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }
    }
}
