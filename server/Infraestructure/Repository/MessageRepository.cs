using Whatsapp_Api.Core.Models;

using Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Whatsapp_Api.Infraestructure.Context;

namespace Whatsapp_Api.Infraestructure.Repository
{
    public class MessageRepository(SocialMediaContext context) 
    {
        public async Task<Message> Create(Message Entity)
        {
            await context.Messages.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<object> GetAllAsync(string ConversationId)
        {
            var messages = await context.Messages
                .Where(p => p.ConversationId == ConversationId)
                .Select(x => new
                {
                    senderId = x.SenderId,
                    receiverId = x.ReceiverId,
                    content = x.Content,
                    date = x.SendDate,
                    read = x.Read
                })
                .OrderBy(x => x.date) // Ordenar por fecha en orden descendente
                .ToListAsync();

            var result = await context.Messages
              .Where(x => x.ConversationId == ConversationId && x.Read == false) // Filtrar mensajes no leídos
              .ExecuteUpdateAsync(x => x.SetProperty(b => b.Read, true));

            Console.WriteLine(result); // Imprime la cantidad de registros actualizados


            return messages;
        }



        public bool Delete(string Id)
        {
            var entity = context.Messages.FirstOrDefault(x => x.Id == Id);

            if (entity == null)
            {
                return false;
            }

            context.Remove(entity);
            context.SaveChanges();

            return true;
        }



        public async Task<Message> Update(Message entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException(nameof(entidad));
            }

            // Buscar la entidad existente en la base de datos
            var entidadExistente = await context.Set<Message>().FindAsync(entidad.GetType().GetProperty("Id")?.GetValue(entidad));
            if (entidadExistente == null)
            {
                throw new InvalidOperationException("La entidad no fue encontrada en la base de datos.");
            }

            // Iterar sobre las propiedades de la entidad
            foreach (var propiedad in entidad.GetType().GetProperties())
            {
                // No actualizar propiedades de solo lectura o claves primarias
                if (!propiedad.CanWrite || propiedad.Name == "Id")
                {
                    continue;
                }

                var valorEntidad = propiedad.GetValue(entidad);
                var valorFinal = valorEntidad ?? propiedad.GetValue(entidadExistente);

                // Asignar el valor final a la entidad existente
                propiedad.SetValue(entidadExistente, valorFinal);
            }

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();

            return entidadExistente;
        }
    }
}
