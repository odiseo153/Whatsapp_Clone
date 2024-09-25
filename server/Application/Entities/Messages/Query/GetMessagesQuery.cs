using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsapp_Api.Infraestructure.Repository;

namespace Whatsapp_Api.Application.Entities.Messages.Query
{
    public class GetMessagesQuery(string id) : IRequest<object>
    {
        public string Id = id;
    }

    public class GetMessagesHandler(MessageRepository repository) : IRequestHandler<GetMessagesQuery, object>
    {
        public async Task<object> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var response = await repository.GetAllAsync(request.Id);

            return response;
        }
    }
}
