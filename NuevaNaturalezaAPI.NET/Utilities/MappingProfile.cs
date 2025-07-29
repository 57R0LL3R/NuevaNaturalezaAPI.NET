using AutoMapper;
using NuevaNaturalezaAPI.NET.Models;
using NuevaNaturalezaAPI.NET.Models.DTO;


namespace NuevaNaturalezaAPI.NET.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ForMember(x=>x.Clave,y=>y.MapFrom(src=>Hash256.Hash(src.Clave)));
        }
    }
}
