using AutoMapper;

namespace NotifierApi.Grpc.Server.Mappers
{
    using Protos;

    using UseCase.Handlers.Command.AddMessage;

    internal sealed class GrpcServerMapProfile : Profile
    {
        public GrpcServerMapProfile()
        {
            CreateMap<SendNotificationRequest, AddMessageCommand>();            
        }
    }
}
