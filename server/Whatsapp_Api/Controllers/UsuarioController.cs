using Application.Entities.Generics.Command;
using Application.Entities.Generics.Query;
using Application.Entities.Usuarios.Command;
using Whatsapp_Api.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Whatsapp_Api.Application.Entities.Usuarios.Command;



namespace Whatsapp_Api.Controllers
{
    [Route("Usuario")]
    public class UsuarioController : BaseApiController
    {
        public UsuarioController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUsuarioCommand user)
        {
            return Ok(await Mediator.Send(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetEntityQuery<User>()));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUsuarioCommand user)
        {
            return Ok(await Mediator.Send(user));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await Mediator.Send(new DeleteEntityCommand<User>(Id)));
        }

    }
}


