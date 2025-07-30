using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class AuditoriumService : IAuditoriumService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public AuditoriumService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditoriumDTO>> GetAllAsync()
        {
            var list = await _context.Auditoria.ToListAsync();
            return _mapper.Map<List<AuditoriumDTO>>(list);
        }

        public async Task<AuditoriumDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Auditoria.FindAsync(id);
            return item == null ? null : _mapper.Map<AuditoriumDTO>(item);
        }

        public async Task<AuditoriumDTO?> CreateAsync(AuditoriumDTO dto)
        {
            var entity = _mapper.Map<Auditorium>(dto);
            _context.Auditoria.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AuditoriumDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, AuditoriumDTO dto)
        {
            if (id != dto.IdAuditoria) return false;

            var entity = _mapper.Map<Auditorium>(dto);
            _context.Entry(entity).State = EntityState.Modified;

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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.Auditoria.FindAsync(id);
            if (item == null) return false;

            _context.Auditoria.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
