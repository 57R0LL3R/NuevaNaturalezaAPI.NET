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
    public class PuntoOptimoService : IPuntoOptimoService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public PuntoOptimoService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PuntoOptimoDTO>> GetAllAsync()
        {
            var lista = await _context.PuntoOptimos.ToListAsync();
            return _mapper.Map<List<PuntoOptimoDTO>>(lista);
        }

        public async Task<PuntoOptimoDTO?> GetByIdAsync(Guid id)
        {
            var item = await _context.PuntoOptimos.FindAsync(id);
            return item == null ? null : _mapper.Map<PuntoOptimoDTO>(item);
        }

        public async Task<PuntoOptimoDTO?> CreateAsync(PuntoOptimoDTO dto)
        {
            var entity = _mapper.Map<PuntoOptimo>(dto);
            _context.PuntoOptimos.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<PuntoOptimoDTO>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, PuntoOptimoDTO dto)
        {
            if (id != dto.IdPuntoOptimo)
                return false;

            var entity = _mapper.Map<PuntoOptimo>(dto);
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
            var entity = await _context.PuntoOptimos.FindAsync(id);
            if (entity == null) return false;

            _context.PuntoOptimos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
