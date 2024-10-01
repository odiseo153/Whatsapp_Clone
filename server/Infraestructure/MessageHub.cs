using Microsoft.AspNetCore.SignalR;
using Whatsapp_Api.Core.Models;


namespace Whatsapp_Api.Infraestructure
{
    public class MessageHub : Hub
    {
        // Método para notificar a los clientes sobre un nuevo mensaje
        public async Task SendMessage(string reciverId,Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", reciverId, message);
        }
    }
}




