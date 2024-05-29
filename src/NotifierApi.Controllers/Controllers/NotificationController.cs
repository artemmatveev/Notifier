namespace NotifierApi.Controllers.Controllers
{
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddNotification;
    using UseCase.Handlers.Command.DeleteNotification;
    using UseCase.Handlers.Command.ChangeNotificationStatus;
    using UseCase.Handlers.Command.UpdateNotification;
    using UseCase.Handlers.Query.FindNotifications;
    using UseCase.Handlers.Query.GetNotification;

    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationController : BaseController<NotificationController>
    {
        readonly IMapper _mapper;

        public NotificationController(IMapper mapper, IMediator mediator)
            : base(mediator)
        {
            _mapper = mapper;
        }


        /// <summary>
        ///     Get applications list by application id
        /// </summary>
        /// <remarks>Get applications list by application id</remarks>
        /// <response code="200">Returns notifications</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet()]
        [ProducesResponseType(typeof(OkResult<Collection<FindNotificationsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<Collection<FindNotificationsResponse>>>> FindNotificationsAsync(
            [FromQuery] FindNotificationsRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<FindNotificationsRequest, FindNotificationsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<Collection<FindNotificationsResponse>>.Create(
                new Collection<FindNotificationsResponse>(_mapper.Map<IReadOnlyList<Domain.Notification>, IReadOnlyList<FindNotificationsResponse>>(result))));
        }


        /// <summary>
        ///     Add a new notification to the store
        /// </summary>
        /// <remarks>Add a new notification to the store</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost()]
        [ProducesResponseType(typeof(OkResult<AddNotificationResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<AddNotificationResponse>>> AddNotificationAsync(
            [FromBody] AddNotificationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddNotificationRequest, AddNotificationCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, OkResult<AddNotificationResponse>.Create(
                _mapper.Map<Domain.Notification, AddNotificationResponse>(result)));
        }


        /// <summary>
        ///     Get notification by notification id
        /// </summary>
        /// <remarks>Get notification by notification id</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OkResult<GetNotificationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OkResult<GetNotificationResponse>>> GetNotificationAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, GetNotificationQuery>(id);
            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, OkResult<GetNotificationResponse>.Create(
                _mapper.Map<Domain.Notification, GetNotificationResponse>(result)));
        }


        /// <summary>
        ///     Updates a notification
        /// </summary>
        /// <remarks>Updates a notification</remarks>
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
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> UpdateNotificationAsync(
            [FromBody] UpdateNotificationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateNotificationRequest, UpdateNotificationCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }

        /// <summary>
        ///     Change a notification status
        /// </summary>
        /// <remarks>Change a notification status</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("status")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> ChangeNotificationStatusAsync(
            [FromBody] ChangeNotificationStatusRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ChangeNotificationStatusRequest, ChangeNotificationStatusCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }


        /// <summary>
        ///     Deletes a notification
        /// </summary>
        /// <remarks>Deletes a notification</remarks>
        /// <response code="200">Ok</response>        
        /// <response code="404">Not Found</response>               
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Iems.Framework.Result.OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Iems.Framework.Result.OkResult>> DeleteNotificationAsync(
            long id, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<long, DeleteNotificationCommand>(id);
            await _mediator.Send(query, cancellationToken);

            return StatusCode(StatusCodes.Status200OK, Iems.Framework.Result.OkResult.Create());
        }
    }
}
