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
    public class ImpactoService : IImpactoService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public ImpactoService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImpactoDTO>> GetAllAsync()
        {
            var impactos = await _context.Impactos.ToListAsync();
            return _mapper.Map<List<ImpactoDTO>>(impactos);
        }

        public async Task<ImpactoDTO?> GetByIdAsync(Guid id)
        {
            var impacto = await _context.Impactos.FindAsync(id);
            return impacto == null ? null : _mapper.Map<ImpactoDTO>(impacto);
        }

        public async Task<ImpactoDTO?> CreateAsync(ImpactoDTO dto)
        {
            var impacto = _mapper.Map<Impacto>(dto);
            _context.Impactos.Add(impacto);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<ImpactoDTO>(impacto);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, ImpactoDTO dto)
        {
            if (id != dto.IdImpacto)
                return false;

            var impacto = _mapper.Map<Impacto>(dto);
            _context.Entry(impacto).State = EntityState.Modified;

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
            var impacto = await _context.Impactos.FindAsync(id);
            if (impacto == null) return false;

            _context.Impactos.Remove(impacto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
