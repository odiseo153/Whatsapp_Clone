using Application;
using Whatsapp_Api.Infraestructure.Context;
using Infraestructure.Repository;
using MediatR;


using Microsoft.EntityFrameworkCore;

namespace Whatsapp_Api.Application.Entities.Messages.Command
{
    public class CreateMessageCommand : IRequest<Response>
    {
        public string? Content { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
    }


    public class CreateMessageHandler(GenericRepository<Message> repository,SocialMediaContext context) : IRequestHandler<CreateMessageCommand, Response>
    {
        public async Task<Response> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = MapperControl.mapper.Map<Message>(request);

            var existingConversation = await context.Conversations
                .FirstOrDefaultAsync(c =>

                    (c.SenderId == message.SenderId && c.ReceiverId == message.ReceiverId) ||
                    (c.SenderId == message.ReceiverId && c.ReceiverId == message.SenderId)
                
                    );

            if (message.ReceiverId == message.SenderId)
            {
                return new Response().Error("No puedes enviarte mensajes a ti mismo");
            }



            if (existingConversation == null)
            {
                var conversation = new Conversation
                {
                    SenderId = message.SenderId,
                    ReceiverId = message.ReceiverId,
                    LastMessage = message.Content,
                };

                message.ConversationId = conversation.Id;

               await context.AddAsync(conversation);
               await context.SaveChangesAsync();

            }

            if(existingConversation != null)
            {
                message.ConversationId = existingConversation.Id;
                message.SenderId = existingConversation.SenderId;
                existingConversation.LastMessage = message.Content;
            }

            var response = await repository.Create(message);

            return new Response().Success(response);

        }
    }
}
