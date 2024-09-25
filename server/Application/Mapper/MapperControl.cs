using Application.Mapper;
using AutoMapper;

namespace Application;

public class MapperControl
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cf =>
        {
            cf.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cf.AddProfile<MappingProfile>();                
        });

            
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper mapper => lazy.Value;
}