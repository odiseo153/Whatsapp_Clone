using MediatR;
using Whatsapp_Api.Infraestructure.Repository;

namespace Whatsapp_Api.Application.Entities.Conversations.Query
{
    public class GetConversationQuery(string idUser) : IRequest<object>
    {
        public string IdUser = idUser;
    }

    public class GetConversationHandler(ConversationRepository repository) : IRequestHandler<GetConversationQuery, object>
    {
        public async Task<object> Handle(GetConversationQuery request, CancellationToken cancellationToken)
        {
            var response = await repository.GetAllAsync(request.IdUser);

            return response;
        }
    }
}
