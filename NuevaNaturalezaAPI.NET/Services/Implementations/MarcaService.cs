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
    public class MarcaService : IMarcaService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public MarcaService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MarcaDTO>> GetAllAsync()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return _mapper.Map<List<MarcaDTO>>(marcas);
        }

        public async Task<MarcaDTO?> GetByIdAsync(Guid id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            return marca == null ? null : _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<MarcaDTO?> CreateAsync(MarcaDTO dto)
        {
            var marca = _mapper.Map<Marca>(dto);
            _context.Marcas.Add(marca);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<MarcaDTO>(marca);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, MarcaDTO dto)
        {
            if (id != dto.IdMarca)
                return false;

            var marca = _mapper.Map<Marca>(dto);
            _context.Entry(marca).State = EntityState.Modified;

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
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return false;

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
