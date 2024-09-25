using Whatsapp_Api.Core.Models;
using Infraestructure.Repository;
using MediatR;

namespace Application.Entities.Generics.Command
{
    public class DeleteEntityCommand<T>(string Id) : IRequest<bool>
    {
        public string Id = Id;
    }

    public class DeleteEntityHandler<T>(GenericRepository<T> repositry) : IRequestHandler<DeleteEntityCommand<T>, bool> where T : class
    {
        public async Task<bool> Handle(DeleteEntityCommand<T> request, CancellationToken cancellationToken)
        {
            var response = repositry.Delete(request.Id);

            return response;
        }
    }
}


