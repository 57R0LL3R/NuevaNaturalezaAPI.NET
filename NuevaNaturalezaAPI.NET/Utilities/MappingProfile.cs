using AutoMapper;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;


namespace NuevaNaturalezaAPI.NET.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ForMember(x=>x.Clave,y=>y.MapFrom(src=>Hash256.Hash(src.Clave))).ReverseMap();

            CreateMap<AuditoriumDTO, Auditorium >().ReverseMap();

            CreateMap<DispositivoDTO, Dispositivo>().ReverseMap();

            CreateMap<EstadoDispositivoDTO, EstadoDispositivo>().ReverseMap();

            CreateMap<EventoDTO, Evento>().ReverseMap();

            CreateMap<FechaMedicionDTO, FechaMedicion>().ReverseMap();

            CreateMap<ImpactoDTO, Impacto>().ReverseMap();

            CreateMap<MarcaDTO, Marca>().ReverseMap();

            CreateMap<MedicionDTO, Medicion>().ReverseMap();

            CreateMap<NotificacionDTO, Notificacion>().ReverseMap();

            CreateMap<PuntoOptimoDTO, PuntoOptimo>().ReverseMap();

            CreateMap<RolDTO, Rol>().ReverseMap();

            CreateMap<SensorDTO, Sensor>().ReverseMap();

            CreateMap<SistemaDTO, Sistema>().ReverseMap();

            CreateMap<TipoDispositivoDTO, TipoDispositivo>().ReverseMap();

            CreateMap<TipoMedicionDTO, TipoMedicion>().ReverseMap();

            CreateMap<TipoMUnidadMDTO, TipoMUnidadM>().ReverseMap();

            CreateMap<TipoNotificacionDTO, TipoNotificacion>().ReverseMap();

            CreateMap<TituloDTO, Titulo>().ReverseMap();

            CreateMap<UnidadMedidumDTO, UnidadMedidum>().ReverseMap();

        }
    }
}
