using Application.Entities.Usuarios.Command;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Whatsapp_Api.Core.Models;
using Whatsapp_Api.Infraestructure.Context;



namespace Whatsapp_Api.Presentation.WebApi.Seed
{
    public static class SeedUser
    {
        private static IMediator _mediator; // Mantengo el campo privado

        // Inicializar IMediator
        public static void Initialize(IMediator mediator)
        {
            _mediator = mediator;
        }
        public static async Task Seed_UserAsync(this SocialMediaContext context, IMediator mediator)
        {

            string path = Path.Combine("Seed", "Data", "User.json");

            if (!path.IsNullOrEmpty())
            {
                var userData = File.ReadAllText(path);
                var users = JsonSerializer.Deserialize<List<CreateUsuarioCommand>>(userData);

                foreach (var user in users)
                {
                    var res =await mediator.Send(user);
                }

                context.SaveChanges();
            }
        }
    }
}
