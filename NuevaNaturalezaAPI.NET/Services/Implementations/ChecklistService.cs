using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.DTOs;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class ChecklistService : IChecklistService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public ChecklistService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChecklistDTO>> GetAllInterval(DateTime desde,DateTime hasta)
        {
            var lista = await _context.Checklists
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.Detalles)
                .ThenInclude(c => c.IdDispositivoNavigation).Where(x=>x.Fecha> desde && x.Fecha< hasta.AddDays(1))
                .ToListAsync();
            return _mapper.Map<List<ChecklistDTO>>(lista);
        }
        public async Task<IEnumerable<ChecklistDTO>> GetAllAsync()
        {
            var lista = await _context.Checklists
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.Detalles)
                .ThenInclude(c => c.IdDispositivoNavigation)
                .ToListAsync();
            return _mapper.Map<List<ChecklistDTO>>(lista);
        }

        public async Task<ChecklistDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Checklists
                .Include(c => c.Detalles)
                .ThenInclude(c => c.IdDispositivoNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(c => c.IdChecklist == id);
            return entity == null ? null : _mapper.Map<ChecklistDTO>(entity);
        }

        public async Task<ChecklistDTO?> CreateAsync(ChecklistDTO dto)
        { 
            if (dto.IdUsuario is null ) {
                    dto.IdUsuario = Guid.Parse("5d78da22-8c43-40f5-aa96-bfe9d531fde8");}
            var entity = _mapper.Map<Checklist>(dto);
            
            _context.Checklists.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ChecklistDTO>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, ChecklistDTO dto)
        {
            if (id != dto.IdChecklist) return false;
            var entity = _mapper.Map<Checklist>(dto);
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
            var entity = await _context.Checklists.FindAsync(id);
            if (entity == null) return false;
            _context.Checklists.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
