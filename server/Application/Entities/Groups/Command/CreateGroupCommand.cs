using Application;
using Infraestructure.Repository;
using MediatR;

namespace Whatsapp_Api.Application.Entities.Groups.Command
{
    public class CreateGroupCommand : IRequest<Response>
    {
        public string Name { get; set; } = null!;

        public string? AdministratorId { get; set; }

        public string? Description { get; set; }

        public string? ImageGroup { get; set; }
    }

    public class CreateGroupHandler(GenericRepository<Group> repository) : IRequestHandler<CreateGroupCommand, Response>
    {
        public async Task<Response> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = MapperControl.mapper.Map<Group>(request);

            var response = await repository.Create(group);

            return new Response().Success(response);
        }
    }
}
