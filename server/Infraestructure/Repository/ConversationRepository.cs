using Whatsapp_Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Whatsapp_Api.Infraestructure.Context;



namespace Whatsapp_Api.Infraestructure.Repository
{
    public class ConversationRepository(SocialMediaContext context)
    {
        public async Task<Conversation> Create(Conversation entity)
        {
           await context.Conversations.AddAsync(entity);
           await context.SaveChangesAsync();
        
           return entity;
        }


        public bool Delete(string id)
        {
            var entity = context.Conversations.FirstOrDefault(x => x.Id == id);

            try
            {
            context.Conversations.Remove(entity);
            context.SaveChanges();

                return true;    
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<object>> GetAllAsync(string idUser)
        {
            var query = context.Conversations
                .Where(p => p.SenderId == idUser || p.ReceiverId == idUser)
                .Select(x => new
                {
                    // Si el usuario es el remitente, devuelve el nombre del destinatario; de lo contrario, devuelve el nombre del remitente
                    id = x.Id,
                    name = x.SenderId == idUser ? x.Receiver.Name : x.Sender.Name,
                    photo = x.SenderId == idUser ? x.Receiver.ProfilePhoto : x.Sender.ProfilePhoto,
                    lastMessage = x.LastMessage,
                    unread = x.Messages.Where(x => x.Read == false).Count()
                });

            return await query.ToListAsync();
        }



        public async Task<Conversation> Update(Conversation entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException(nameof(entidad));
            }

            // Buscar la entidad existente en la base de datos
            var entidadExistente = await context.Conversations.FindAsync(entidad.GetType().GetProperty("Id")?.GetValue(entidad));
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
