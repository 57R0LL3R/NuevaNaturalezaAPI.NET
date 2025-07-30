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
    public class FechaMedicionService : IFechaMedicionService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public FechaMedicionService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FechaMedicionDTO>> GetAllAsync()
        {
            var fechas = await _context.FechaMedicions.ToListAsync();
            return _mapper.Map<List<FechaMedicionDTO>>(fechas);
        }

        public async Task<FechaMedicionDTO?> GetByIdAsync(Guid id)
        {
            var fecha = await _context.FechaMedicions.FindAsync(id);
            return fecha == null ? null : _mapper.Map<FechaMedicionDTO>(fecha);
        }

        public async Task<FechaMedicionDTO?> CreateAsync(FechaMedicionDTO dto)
        {
            var fecha = _mapper.Map<FechaMedicion>(dto);
            _context.FechaMedicions.Add(fecha);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<FechaMedicionDTO>(fecha);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, FechaMedicionDTO dto)
        {
            if (id != dto.IdFechaMedicion) return false;

            var fecha = _mapper.Map<FechaMedicion>(dto);
            _context.Entry(fecha).State = EntityState.Modified;

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
            var fecha = await _context.FechaMedicions.FindAsync(id);
            if (fecha == null) return false;

            _context.FechaMedicions.Remove(fecha);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
