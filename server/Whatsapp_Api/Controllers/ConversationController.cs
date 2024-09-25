using MediatR;
using Microsoft.AspNetCore.Mvc;
using Whatsapp_Api.Application.Entities.Conversations.Query;


namespace Whatsapp_Api.Presentation.WebApi.Controllers
{
    [Route("Conversation")]
    public class ConversationController : BaseApiController
    {
        public ConversationController(IMediator mediator) : base(mediator)
        {

        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var response = await Mediator.Send(new GetConversationQuery(userId));
                        
            return Ok(response);
        }
    }
}




