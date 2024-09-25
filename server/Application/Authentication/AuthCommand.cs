using Whatsapp_Api.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace Application.Authentication
{
    public class AuthCommand : IRequest<Response>
    {
        public string Phone  { get; set; }
    }

    public class AuthHandler(SocialMediaContext context,IConfiguration configuration) : IRequestHandler<AuthCommand, Response>
    {
        public async Task<Response> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            // Verifica si el usuario existe
            var user = await context.Users.FirstOrDefaultAsync(x => x.Phone == request.Phone);

            if (user == null)
            {
                 return new Response().Error("No existe un usuario con ese numero de telefono");     
            }

            var token = new JWT(configuration).GenerateJwtToken(user);

            return new Response().Success(user, $"{token}");       

        }

       

    }
}








