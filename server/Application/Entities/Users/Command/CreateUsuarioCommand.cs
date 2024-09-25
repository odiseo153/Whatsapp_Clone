using Whatsapp_Api.Core.Models;

using Whatsapp_Api.Infraestructure.Context;
using Infraestructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Application.Entities.Usuarios.Command
{
    public class CreateUsuarioCommand : IRequest<Response>
    {
       public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? ProfilePhoto { get; set; }
    }


    public class CreateUsuarioHandler(GenericRepository<User> repository,SocialMediaContext context) : IRequestHandler<CreateUsuarioCommand, Response>
    {
        public async Task<Response> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = MapperControl.mapper.Map<User>(request);
            var existe = await context.Users.FirstOrDefaultAsync(x=> x.Phone == request.Phone);

            if(existe != null)
            {
                return new Response().Error("Ya hay alguien con ese numero de telefono");
            }

            var user = await repository.Create(usuario);

            return new Response().Success(user, "Usuario Creado Correctamente");
        }
    }
}
