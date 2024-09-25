using Whatsapp_Api.Infraestructure.Context;
using System.Text.Json;
using MediatR;
using Whatsapp_Api.Application.Entities.Messages.Command;

namespace Whatsapp_Api.Presentation.WebApi.Seed
{
    public static class SeedMessage
    {
        private static IMediator _mediator; // Mantengo el campo privado

        // Inicializar IMediator
        public static void Initialize(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Convertir Seed_Messages a async para manejar la asincronía
        public static async Task Seed_MessagesAsync(this SocialMediaContext context, IMediator mediator)
        {
            string path = Path.Combine("Seed", "Data", "Message.json");

            if (!string.IsNullOrEmpty(path))
            {
                var citasData = await File.ReadAllTextAsync(path); // Leer el archivo de forma asincrónica
                var messages = JsonSerializer.Deserialize<List<CreateMessageCommand>>(citasData);

                foreach (var message in messages)
                {
                    var res = await mediator.Send(message); // Esperar a que se envíe el comando
                }
            }
        }
    }
}
