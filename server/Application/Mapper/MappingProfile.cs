using Application.Entities.Usuarios.Command;
using AutoMapper;
using Whatsapp_Api.Core.Models;
using Whatsapp_Api.Application.Entities.Groups.Command;
using Whatsapp_Api.Application.Entities.Messages.Command;
using Whatsapp_Api.Application.Entities.Usuarios.Command;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          CreateMap<CreateUsuarioCommand, User>().ReverseMap();
          CreateMap<UpdateUsuarioCommand, User>().ReverseMap();
          CreateMap<CreateMessageCommand,Message>().ReverseMap();
            CreateMap<CreateGroupCommand, Group>().ReverseMap();

        }
    }
}



