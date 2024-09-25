using Application.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace Whatsapp_Api.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : BaseApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthCommand command)
        {
            var response = await Mediator.Send(command);

            if (response == null)
            {
                return NotFound("User not found.");
            }

            return Ok(response);    
        }

    }
}






