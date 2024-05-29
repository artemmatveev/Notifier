namespace NotifierApi.Controllers.Mappers
{
    using Domain;    
    using Rest.V1.S0;
    using UseCase.Handlers.Command.AddApplication;
    using UseCase.Handlers.Command.AddChannel;
    using UseCase.Handlers.Command.AddConvention;
    using UseCase.Handlers.Command.AddMessage;
    using UseCase.Handlers.Command.AddNotification;
    using UseCase.Handlers.Command.AddTemplate;
    using UseCase.Handlers.Command.DeleteApplication;
    using UseCase.Handlers.Command.DeleteChannel;
    using UseCase.Handlers.Command.DeleteNotification;
    using UseCase.Handlers.Command.DeleteTemplate;
    using UseCase.Handlers.Command.ChangeApplicationStatus;
    using UseCase.Handlers.Command.ChangeChannelStatus;
    using UseCase.Handlers.Command.EnableConvention;
    using UseCase.Handlers.Command.ChangeNotificationStatus;
    using UseCase.Handlers.Command.ChangeTemplateStatus;
    using UseCase.Handlers.Command.Transform;
    using UseCase.Handlers.Command.UpdateApplication;
    using UseCase.Handlers.Command.UpdateChannel;
    using UseCase.Handlers.Command.UpdateNotification;
    using UseCase.Handlers.Command.UpdateTemplate;
    using UseCase.Handlers.Command.UpdateTemplateContent;    
    using UseCase.Handlers.Query.FindApplications;
    using UseCase.Handlers.Query.FindChannels;
    using UseCase.Handlers.Query.FindConventions;
    using UseCase.Handlers.Query.FindEmailMessages;
    using UseCase.Handlers.Query.FindNotifications;
    using UseCase.Handlers.Query.FindTelegramMessages;
    using UseCase.Handlers.Query.FindTemplates;
    using UseCase.Handlers.Query.GetApplication;
    using UseCase.Handlers.Query.GetChannel;
    using UseCase.Handlers.Query.GetNotification;
    using UseCase.Handlers.Query.GetResource;
    using UseCase.Handlers.Query.GetTemplate;
    using UseCase.Handlers.Command.UpdateResource;
    using UseCase.Handlers.Query.FindResources;
    using NotifierApi.Rest.V1.S0.Template;
    using NotifierApi.UseCase.Handlers.Query.GetContent;
    using NotifierApi.UseCase.Handlers.Query.FindBitrix24Messages;

    internal sealed class ControllerMapProfile : Profile
    {
        public ControllerMapProfile()
        {
            // Application:
            CreateMap<AddApplicationRequest, AddApplicationCommand>();
            CreateMap<Application, AddApplicationResponse>();
            CreateMap<long, GetApplicationQuery>().ConstructUsing(t => new GetApplicationQuery(t));
            CreateMap<Application, GetApplicationResponse>();
            CreateMap<UpdateApplicationRequest, UpdateApplicationCommand>();
            CreateMap<long, DeleteApplicationCommand>().ConstructUsing(t => new DeleteApplicationCommand(t));
            CreateMap<FindApplicationsRequest, FindApplicationsQuery>();
            CreateMap<Application, FindApplicationsResponse>();
            CreateMap<ChangeApplicationStatusRequest, ChangeApplicationStatusCommand>();

            // Channel:
            CreateMap<AddChannelRequest, AddChannelCommand>();
            CreateMap<Channel, AddChannelResponse>();
            CreateMap<long, GetChannelQuery>().ConstructUsing(t => new GetChannelQuery(t));
            CreateMap<Channel, GetChannelResponse>();
            CreateMap<UpdateChannelRequest, UpdateChannelCommand>();
            CreateMap<long, DeleteChannelCommand>().ConstructUsing(t => new DeleteChannelCommand(t));
            CreateMap<FindChannelsRequest, FindChannelsQuery>();
            CreateMap<Channel, FindChannelsResponse>();
            CreateMap<ChangeChannelStatusRequest, ChangeChannelStatusCommand>();

            // Notification:
            CreateMap<AddNotificationRequest, AddNotificationCommand>();
            CreateMap<Notification, AddNotificationResponse>();
            CreateMap<long, GetNotificationQuery>().ConstructUsing(t => new GetNotificationQuery(t));
            CreateMap<Notification, GetNotificationResponse>();
            CreateMap<UpdateNotificationRequest, UpdateNotificationCommand>();
            CreateMap<long, DeleteNotificationCommand>().ConstructUsing(t => new DeleteNotificationCommand(t));
            CreateMap<FindNotificationsRequest, FindNotificationsQuery>();
            CreateMap<Notification, FindNotificationsResponse>();
            CreateMap<ChangeNotificationStatusRequest, ChangeNotificationStatusCommand>();

            // Template:
            CreateMap<AddTemplateRequest, AddTemplateCommand>();
            CreateMap<Template, AddTemplateResponse>();
            CreateMap<long, GetTemplateQuery>().ConstructUsing(t => new GetTemplateQuery(t));
            CreateMap<Template, GetTemplateResponse>();
            CreateMap<UpdateTemplateRequest, UpdateTemplateCommand>();
            CreateMap<UpdateTemplateContentRequest, UpdateTemplateContentCommand>();
            CreateMap<long, DeleteTemplateCommand>().ConstructUsing(t => new DeleteTemplateCommand(t));
            CreateMap<FindTemplatesRequest, FindTemplatesQuery>();
            CreateMap<Template, FindTemplatesResponse>();
            CreateMap<TransformRequest, TransformCommand>();
            CreateMap<ChangeTemplateStatusRequest, ChangeTemplateStatusCommand>();
            CreateMap<long, GetTemplateContentQuery>().ConstructUsing(t => new GetTemplateContentQuery(t));
            CreateMap<Template, GetTemplateContentResponse>();


            // Convention:
            CreateMap<AddConventionRequest, AddConventionCommand>();
            CreateMap<Convention, AddConventionResponse>();
            CreateMap<EnableConventionRequest, EnableConventionCommand>();
            CreateMap<FindConventionsRequest, FindConventionsQuery>();
            CreateMap<Convention, FindConventionsResponse>();

            // Resource:
            CreateMap<long, GetResourceQuery>().ConstructUsing(t => new GetResourceQuery(t));
            CreateMap<Resource, GetResourceResponse>();
            CreateMap<UpdateResourceRequest, UpdateResourceCommand>();
            CreateMap<FindResourcesRequest, FindResourcesQuery>();
            CreateMap<Resource, FindResourcesResponse>();

            // OutboxMessage:
            CreateMap<AddMessageRequest, AddMessageCommand>();

            // EmailMessage:
            CreateMap<FindEmailMessagesRequest, FindEmailMessagesQuery>();
            CreateMap<EmailMessage, FindEmailMessagesResponse>();

            // TelegramMessage:
            CreateMap<FindTelegramMessagesRequest, FindTelegramMessagesQuery>();
            CreateMap<TelegramMessage, FindTelegramMessagesResponse>();

            // Bitrix24Message:
            CreateMap<FindBitrix24MessagesRequest, FindBitrix24MessagesQuery>();
            CreateMap<Bitrix24Message, FindBitrix24MessagesResponse>();

        }
    }
}
