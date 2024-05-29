namespace NotifierApi.Controllers.Controllers
{
    using NotifierApi.UseCase.Handlers.Query.FindBitrix24Messages;
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddMessage;
    using UseCase.Handlers.Query.FindEmailMessages;
    using UseCase.Handlers.Query.FindTelegramMessages;

    [Route("api/v1/outbox")]
    [ApiController]
    public sealed class OutboxController : BaseController<OutboxController>
    {
        readonly IMapper _mapper;

        public OutboxController(IMapper mapper, IMediator mediator)
            : base(mediator)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///     Add a messages to the store
        /// </summary>
        /// <remarks>Add a messages to the store</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost()]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Iems.Framework.Result.OkResult>>> AddMessagesAsync(
            [FromBody] AddMessageRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddMessageRequest, AddMessageCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Get email messages list
        /// </summary>
        /// <remarks>Get email messages list</remarks>
        /// <response code="200">Returns email messages</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("email")]
        [ProducesResponseType(typeof(OkResult<Page<FindEmailMessagesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Page<FindEmailMessagesResponse>>>> FindApplicationsAsync(
            [FromQuery] FindEmailMessagesRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindEmailMessagesRequest, FindEmailMessagesQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Page<FindEmailMessagesResponse>>.Create(
                new Page<FindEmailMessagesResponse>(
                    _mapper.Map<IReadOnlyList<Domain.EmailMessage>, IReadOnlyList<FindEmailMessagesResponse>>(result.EmailMessages),
                    request.PageNumber,
                    request.PageSize,
                    result.TotalRecords
                )));
        }

        /// <summary>
        ///     Get telegram messages list
        /// </summary>
        /// <remarks>Get telegram messages list</remarks>
        /// <response code="200">Returns telegram messages</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("telegram")]
        [ProducesResponseType(typeof(OkResult<Page<FindTelegramMessagesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Page<FindTelegramMessagesResponse>>>> FindApplicationsAsync(
            [FromQuery] FindTelegramMessagesRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindTelegramMessagesRequest, FindTelegramMessagesQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Page<FindTelegramMessagesResponse>>.Create(
                new Page<FindTelegramMessagesResponse>(
                    _mapper.Map<IReadOnlyList<Domain.TelegramMessage>, IReadOnlyList<FindTelegramMessagesResponse>>(result.TelegramMessages),
                    request.PageNumber,
                    request.PageSize,
                    result.TotalRecords
                )));
        }

        /// <summary>
        ///     Get bitrix24 messages list
        /// </summary>
        /// <remarks>Get bitrix24 messages list</remarks>
        /// <response code="200">Returns bitrix24 messages</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("bitrix24")]
        [ProducesResponseType(typeof(OkResult<Page<FindBitrix24MessagesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Page<FindBitrix24MessagesResponse>>>> FindBitrix24Async(
            [FromQuery] FindBitrix24MessagesRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindBitrix24MessagesRequest, FindBitrix24MessagesQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Page<FindBitrix24MessagesResponse>>.Create(
                new Page<FindBitrix24MessagesResponse>(
                    _mapper.Map<IReadOnlyList<Domain.Bitrix24Message>, IReadOnlyList<FindBitrix24MessagesResponse>>(result.Bitrix24Messages),
                    request.PageNumber,
                    request.PageSize,
                    result.TotalRecords
                )));
        }
    }
}
