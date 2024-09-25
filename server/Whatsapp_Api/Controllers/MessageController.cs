using Application.Entities.Generics.Command;
using Application.Entities.Generics.Query;
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
            return Ok(await Mediator.Send(message));
        }

        [HttpGet("{IdConversation}")]
        public async Task<IActionResult> Get(string IdConversation)
        {
            return Ok(await Mediator.Send(new GetMessagesQuery(IdConversation)));
        }

        [HttpDelete] 
        public IActionResult Delete(string Id)
        {
            return Ok(Mediator.Send(new DeleteEntityCommand<Message>(Id)));
        }
    }
}
