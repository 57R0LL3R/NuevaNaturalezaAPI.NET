using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuevaNaturalezaAPI.NET.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly NuevaNatuContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(NuevaNatuContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.Include(x=>x.IdRolNavigation).ToListAsync();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO?> GetByIdAsync(Guid id)
        {
            Usuario? usuario = await _context.Usuarios.Include(x => x.IdRolNavigation).FirstOrDefaultAsync(x=>x.IdUsuario==id);
            return usuario == null ? null : _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO?> CreateAsync(UsuarioDTO dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<UsuarioDTO>(usuario);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, UsuarioDTO dto)
        {

            var usu = _context.Usuarios.Find(id);
            if (id != dto.IdUsuario || usu is null) return false;

            var usuario = _mapper.Map<Usuario>(dto);
            if (usuario.Clave != null)
            {
               usu.Clave = Hash256.Hash(usuario.Clave);
            }
            usu.IdRol = usuario.IdRol;
            usu.Cedula = usuario.Cedula;
            usu.Correo = usuario.Correo;
            usu.Nombre = usuario.Nombre;

            _context.Entry(usu).State = EntityState.Modified;

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
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
