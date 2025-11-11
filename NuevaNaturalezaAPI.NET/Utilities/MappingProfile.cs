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

            CreateMap<Area, AreaDTO>().ReverseMap();

            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>().ForMember(dest=>dest.Clave,opt=>opt.Ignore());

            CreateMap<AuditoriumDTO, Auditorium >();
            CreateMap<Auditorium, AuditoriumDTO>()
                .ForMember(dest => dest.AccionNombre, opt => opt.MapFrom(x => x.IdAccionNavigation.Accion ?? "N/A"))
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(x => x.IdUsuarioNavigation.Nombre ?? "N/A"))
                .ForMember(dest => dest.DispositivoNombre, opt => opt.MapFrom(x => x.IdDispositivoNavigation.Nombre ?? "N/A"));

            CreateMap<ActuadorDTO, Actuador>().ReverseMap();
            CreateMap<AccionActDTO, AccionAct>().ReverseMap();


            CreateMap<DispositivoDTO, Dispositivo>();
            CreateMap< Dispositivo,DispositivoDTO>().
                ForMember(dest => dest.Estado, opt => opt.MapFrom(x => x.IdEstadoDispositivoNavigation.Nombre ?? "N/A"))
                .ForMember(dest => dest.Sistema, opt => opt.MapFrom(x => x.IdSistemaNavigation.Nombre ?? "N/A"));
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

            // Mapea los detalles
            CreateMap<ChecklistDetalleDTO, ChecklistDetalle>()
                .ForMember(dest => dest.IdDetalle, opt => opt.MapFrom(src => src.IdChecklistDetalle))
                .ForMember(dest => dest.IdDispositivo, opt => opt.MapFrom(src => src.IdDispositivo))
                .ForMember(dest => dest.ValorRegistrado, opt => opt.MapFrom(src =>
                (src.EstadoActuador == null ? src.ValorRegistrado.Length > 0 ? src.ValorRegistrado : "N/A" : src.EstadoActuador.Value ? "Encendido": "Apagado")
                   ))
                .ForMember(dest => dest.Tipo, opt => opt.Ignore())
                .ForMember(dest => dest.Checklist, opt => opt.Ignore())
                .ForMember(dest => dest.IdDispositivoNavigation, opt => opt.Ignore());

            CreateMap<ChecklistDetalle, ChecklistDetalleDTO>()
                .ForMember(dest => dest.IdChecklistDetalle, opt => opt.MapFrom(src => src.IdDetalle))
                .ForMember(dest => dest.IdDispositivo, opt => opt.MapFrom(src => src.IdDispositivo))
                .ForMember(dest => dest.NombreDispositivo, opt => opt.MapFrom(src => src.IdDispositivoNavigation != null ? src.IdDispositivoNavigation.Nombre : "N/A"))
                .ForMember(dest => dest.ValorRegistrado, opt => opt.MapFrom(src => src.ValorRegistrado))
                .ForMember(dest => dest.EstadoActuador, opt => opt.Ignore());

            // Mapea el checklist principal y su lista de detalles
            CreateMap<ChecklistDTO, Checklist>()
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

            CreateMap< Checklist, ChecklistDTO>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.IdUsuarioNavigation.Nombre));


            CreateMap<ProgramacionDosificador, ProgramacionDosificadorDTO>()
                .ForMember(dest => dest.NombreDosificador, opt => opt.MapFrom(src => src.Dosificador != null ? src.Dosificador.IdDispositivoNavigation.Nombre : src.Dosificador.LetraActivacion))
                .ReverseMap()
                .ForMember(dest => dest.Dosificador, opt => opt.Ignore());

            CreateMap<DosificadorDTO, Dosificador>().ReverseMap();

        }
    }
}
