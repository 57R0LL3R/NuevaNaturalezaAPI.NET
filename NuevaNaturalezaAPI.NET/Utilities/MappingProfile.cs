using AutoMapper;
using NuevaNaturalezaAPI.NET.DTOs;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;


namespace NuevaNaturalezaAPI.NET.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>().ForMember(dest=>dest.Clave,opt=>opt.Ignore());

            CreateMap<AuditoriumDTO, Auditorium >();
            CreateMap<Auditorium, AuditoriumDTO>()
                .ForMember(dest => dest.AccionNombre, opt => opt.MapFrom(x => x.IdAccionNavigation.Accion ?? "N/A"))
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(x => x.IdUsuarioNavigation.Nombre ?? "N/A"))
                .ForMember(dest => dest.DispositivoNombre, opt => opt.MapFrom(x => x.IdDispositivoNavigation.Nombre ?? "N/A"));

            CreateMap<ActuadorDTO, Actuador>().ReverseMap();
            CreateMap<AccionActDTO, AccionAct>().ReverseMap();


            CreateMap<DispositivoDTO, Dispositivo>().ReverseMap();
            CreateMap<TipoExcesoDTO, TipoExceso>().ReverseMap();

            CreateMap<ExcesoPuntoOptimoDTO, ExcesoPuntoOptimo>().ReverseMap();


            CreateMap<EstadoDispositivoDTO, EstadoDispositivo>().ReverseMap();

            CreateMap<EventoDTO, Evento>().ReverseMap();

            CreateMap<FechaMedicionDTO, FechaMedicion>().ReverseMap();

            CreateMap<ImpactoDTO, Impacto>().ReverseMap();

            CreateMap<MarcaDTO, Marca>().ReverseMap();

            CreateMap<MedicionDTO, Medicion>().ReverseMap();
            CreateMap<Medicion, MedicionDTO>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(x => x.IdFechaMedicionNavigation.Fecha));

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

            CreateMap<SugerenciaDTO, Sugerencia>().ReverseMap();

            CreateMap<ChecklistDTO, Checklist>().ReverseMap();

            CreateMap<ProgramacionDosificador, ProgramacionDosificadorDTO>()
                .ForMember(dest => dest.NombreDosificador, opt => opt.MapFrom(src => src.Dosificador != null ? src.Dosificador.IdDispositivoNavigation.Nombre : src.Dosificador.LetraActivacion))
                .ReverseMap()
                .ForMember(dest => dest.Dosificador, opt => opt.Ignore());

            CreateMap<DosificadorDTO, Dosificador>().ReverseMap();

        }
    }
}
