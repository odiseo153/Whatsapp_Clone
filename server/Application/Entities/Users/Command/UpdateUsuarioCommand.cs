using Application;
using Whatsapp_Api.Core.Models;

using Infraestructure.Repository;
using MediatR;

namespace Whatsapp_Api.Application.Entities.Usuarios.Command
{
    public class UpdateUsuarioCommand : IRequest<Response>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePhoto { get; set; }
    }

    public class UpdateUsuarioHandler(GenericRepository<User> repository) : IRequestHandler<UpdateUsuarioCommand, Response>
    {
        public async Task<Response> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = MapperControl.mapper.Map<User>(request);

            var response = await repository.Update(usuario);

            return new Response().Success(response);
        }
    }
}
