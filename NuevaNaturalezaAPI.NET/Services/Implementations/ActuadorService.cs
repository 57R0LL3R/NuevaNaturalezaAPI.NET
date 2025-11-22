using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using System.Security.Claims;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ActuadorService(NuevaNatuContext context, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : IActuadorService
    {
        private readonly NuevaNatuContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<IEnumerable<ActuadorDTO>> GetAllAsync()
        {
            var list = await _context.Actuador.Include(x => x.IdAccionActNavigation).ToListAsync();
            return _mapper.Map<List<ActuadorDTO>>(list);
        }

        public async Task<ActuadorDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Actuador.Include(x => x.IdAccionActNavigation).FirstOrDefaultAsync(x=>x.IdActuador== id);
            return item == null ? null : _mapper.Map<ActuadorDTO>(item);
        }

        public async Task<ActuadorDTO?> CreateAsync(ActuadorDTO dto)
        {
            var entity = _mapper.Map<Actuador>(dto);
            _context.Actuador.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ActuadorDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ActuadorDTO dto)
        {
            if (id != dto.IdActuador) return false;
            Actuador? actuador = await _context.Actuador.FirstOrDefaultAsync(x=>x.IdActuador==id);
            if (actuador == null) return false;
            actuador.Off = dto.Off==string.Empty? actuador.Off : dto.Off;
            actuador.On = dto.On == string.Empty ? actuador.On : dto.On;

            _context.Entry(actuador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //await _context.SaveChangesAsync();
        public async Task<Response?> ONOFFActuador(Guid id, ActuadorDTO dto, Guid? idSistema,string observacion)
        {

            Actuador? actuador = await _context.Actuador.FirstOrDefaultAsync(x => x.IdActuador == id);
            if (id != dto.IdActuador || actuador is null) return null;
            if (dto.IdAccionAct != actuador.IdAccionAct)
            {
                var audi = await _context.Auditoria.FirstOrDefaultAsync(x => x.IdDispositivo == dto.IdDispositivo && x.Estado == (int)NumberStatus.InProcces);
                if (audi == null)
                {

                    var userId = _httpContextAccessor.HttpContext?
                        .User?
                        .FindFirst(ClaimTypes.NameIdentifier)?
                        .Value;
                    userId ??= "5d78da22-8c43-40f5-aa96-bfe9d531fde8";
                    await _context.Auditoria.AddAsync(new Auditorium()
                    {
                        Estado = (int)NumberStatus.InProcces,
                        IdAccion = dto.IdAccionAct,
                        IdDispositivo = dto.IdDispositivo,
                        Observacion = observacion,
                        IdUsuario = Guid.Parse(userId)
                    });
                    await _context.SaveChangesAsync();
                    await _context.Eventos.AddAsync(new Evento()
                    {
                        IdImpacto = Guid.Parse("ec5e89b7-d35f-4925-900e-6dafe45e5470"),
                        IdAccionAct = dto.IdAccionAct,
                        IdDispositivo = dto.IdDispositivo,
                        IdSistema = idSistema ?? Guid.Parse("1f1b289a-5fc7-426a-937c-1475c168d2f4")
                    });
                    await _context.SaveChangesAsync();

                    return new()
                    {
                        NumberResponse = (int)NumberResponses.Correct,
                        Data = actuador,
                        Message = "Auditoria creada se procesara en breves instantes"
                    };
                }
                return new()
                {
                    NumberResponse = (int)NumberResponses.Warning,
                    Data = actuador,
                    Message = "No se creo auditoria ya hay un proceso pendiente"
                };

            }
            return new()
            {
                NumberResponse=(int)NumberResponses.Incorrect,
                Data = actuador,
                Message = "No se creo auditoria la accion solicitada es opuesta a lo esperado"
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.Actuador.FindAsync(id);
            if (item == null) return false;

            _context.Actuador.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
