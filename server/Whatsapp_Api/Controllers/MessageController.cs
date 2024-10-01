using Application.Entities.Generics.Command;
using Whatsapp_Api.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Whatsapp_Api.Application.Entities.Messages.Command;
using Whatsapp_Api.Application.Entities.Messages.Query;


namespace Whatsapp_Api.Presentation.WebApi.Controllers
{
    [Route("Message")]
    public class MessageController : BaseApiController
    {
        public MessageController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageCommand message)
        {
           // message.Image = "https://media.revistagq.com/photos/65cf47440b12924e35f93c92/16:9/w_1824,h_1026,c_limit/Screenshot%202024-02-15%20at%209.11.28%20PM.png";
            return Ok(await Mediator.Send(message));
        }

        [HttpGet("{IdConversation}")]
        public async Task<IActionResult> Get(string IdConversation)
        {
            return Ok(await Mediator.Send(new GetMessagesQuery(IdConversation)));
        }

        [HttpDelete("{Id}")] 
        public IActionResult Delete(string Id)
        {
            return Ok(Mediator.Send(new DeleteEntityCommand<Message>(Id)));
        }
    }
}
