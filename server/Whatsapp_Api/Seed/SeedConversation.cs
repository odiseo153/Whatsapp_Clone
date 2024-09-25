
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Whatsapp_Api.Core.Models;
using Whatsapp_Api.Infraestructure.Context;


namespace Whatsapp_Api.Presentation.WebApi.Seed
{
    public static class SeedConversation
    {
        public static void Seed_Conversation(this SocialMediaContext context)
        {

            string path = Path.Combine("Seed", "Data", "Conversation.json");

            if (!path.IsNullOrEmpty())
            {
                var pacienteData = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<Conversation>>(pacienteData);
                //var pacientes = MapperControl.mapper.Map<List<Paciente>>(pacientesMap);

                foreach (var paciente in data)
                {
                    var pacienteExiste = context.Conversations.Find(paciente.Id);

                    if (pacienteExiste == null && paciente != null)
                    {
                      context.Conversations.Add(paciente);
                    }

                }
              
                context.SaveChanges();

                
            }
        }
    }
}
