using Application.Authentication;
using Application.Entities.Generics.Command;
using Application.Entities.Generics.Query;
using Application.Entities.Usuarios.Command;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Whatsapp_Api.Application.Entities.Conversations.Query;
using Whatsapp_Api.Application.Entities.Groups.Command;
using Whatsapp_Api.Application.Entities.Messages.Command;
using Whatsapp_Api.Application.Entities.Messages.Query;
using Whatsapp_Api.Application.Entities.Usuarios.Command;

namespace Application
{
    public static class ServiceRegistation
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IRequestHandler<AuthCommand, Response>, AuthHandler>();
            services.AddScoped<IRequestHandler<CreateUsuarioCommand, Response>, CreateUsuarioHandler>();

            services.AddScoped<IRequestHandler<GetEntityQuery<User>, IEnumerable<User>>, GetEntityHandler<User>>();
            services.AddScoped<IRequestHandler<GetEntityQuery<Message>, IEnumerable<Message>>, GetEntityHandler<Message>>();
            services.AddScoped<IRequestHandler<GetEntityQuery<Group>, IEnumerable<Group>>, GetEntityHandler<Group>>();
            services.AddScoped<IRequestHandler<GetEntityQuery<Conversation>, IEnumerable<Conversation>>, GetEntityHandler<Conversation>>();

            services.AddScoped<IRequestHandler<GetMessagesQuery, object>, GetMessagesHandler>();
            services.AddScoped<IRequestHandler<GetConversationQuery, object>, GetConversationHandler>();

            services.AddScoped<IRequestHandler<UpdateUsuarioCommand, Response>, UpdateUsuarioHandler>();
            services.AddScoped<IRequestHandler<DeleteEntityCommand<User>, bool>, DeleteEntityHandler<User>>();
            services.AddScoped<IRequestHandler<DeleteEntityCommand<Message>, bool>, DeleteEntityHandler<Message>>();
            services.AddScoped<IRequestHandler<CreateMessageCommand, Response>, CreateMessageHandler>();
            services.AddScoped<IRequestHandler<CreateGroupCommand, Response>, CreateGroupHandler>();


        }

    }
}
