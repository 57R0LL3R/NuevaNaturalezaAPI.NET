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
    public class MedicionService : IMedicionService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public MedicionService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicionDTO>> GetAllAsync()
        {
            var lista = await _context.Medicions.ToListAsync();
            return _mapper.Map<List<MedicionDTO>>(lista);
        }

        public async Task<MedicionDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.Medicions.FindAsync(id);
            return item == null ? null : _mapper.Map<MedicionDTO>(item);
        }

        public async Task<MedicionDTO?> CreateAsync(MedicionDTO dto)
        {
            var entity = _mapper.Map<Medicion>(dto);
            _context.Medicions.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<MedicionDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, MedicionDTO dto)
        {
            if (id != dto.IdMedicion)
                return false;

            var entity = _mapper.Map<Medicion>(dto);
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
            var entity = await _context.Medicions.FindAsync(id);
            if (entity == null) return false;

            _context.Medicions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
