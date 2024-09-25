using Application.Entities.Generics.Query;
using Whatsapp_Api.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Whatsapp_Api.Application.Entities.Groups.Command;

namespace Whatsapp_Api.Presentation.WebApi.Controllers
{
    [Route("Gruop")]
    public class GroupController : BaseApiController
    {
        public GroupController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupCommand Group)
        {
            return Ok(await Mediator.Send(Group));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetEntityQuery<Group>()));
        }
    }
}
