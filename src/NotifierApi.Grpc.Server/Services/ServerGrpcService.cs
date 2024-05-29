using AutoMapper;
using MediatR;

namespace NotifierApi.Grpc.Server.Services
{
    using System.Threading.Tasks;
    using NotifierApi.UseCase.Handlers.Command.AddMessage;
    using Protos;

    internal partial class ServerGrpcService
        : NotifierApiGrpcService.NotifierApiGrpcServiceBase
    {
        readonly IMapper _mapper;
        readonly IMediator _mediator;

        public ServerGrpcService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task<SendNotificationResponse> SendNotification(SendNotificationRequest request, 
            global::Grpc.Core.ServerCallContext context)
        {
            var command = _mapper.Map<SendNotificationRequest, AddMessageCommand>(request);
            await _mediator.Send(command);

            return new SendNotificationResponse();

        }
    }
}
