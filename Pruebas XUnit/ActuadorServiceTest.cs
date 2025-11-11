using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Models.DTO;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;

namespace Pruebas_XUnit
{
    public class ActuadorServiceTest

    {

        private readonly NuevaNatuContext _context;
        private readonly Mock<IMapper> _mapper;
        private readonly ActuadorService _service;

        readonly ActuadorDTO ActuadorDTO = new() {
            IdActuador = Guid.Parse("06e92067-9d17-4507-8e79-055587857251"),
            IdDispositivo = Guid.NewGuid(),
            IdAccionAct = Guid.NewGuid(),
            Off= "",
            On = "" 
        };
        readonly Actuador Actuador = new()
        {
            IdActuador = Guid.Parse("06e92067-9d17-4507-8e79-055587857251"),
            IdDispositivo = Guid.NewGuid(),
            IdAccionAct = Guid.NewGuid(),
            Off = "",
            On = ""
        };
        public ActuadorServiceTest()
        {
            var options = new DbContextOptionsBuilder<NuevaNatuContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new NuevaNatuContext(options);


            _mapper = new Mock<IMapper>();

            _mapper.Setup(m => m.Map<List<Actuador>>(It.IsAny<List<ActuadorDTO>>()))
                   .Returns(new List<Actuador>());

            // Cuando el mapper reciba una entidad → devolverá un DTO de vuelta
            _mapper.Setup(m => m.Map<List<ActuadorDTO>>(It.IsAny<List<Actuador>>()))
                   .Returns(new List<ActuadorDTO> { });


            _mapper.Setup(m => m.Map<Actuador>(It.IsAny<ActuadorDTO>()))
                   .Returns(Actuador); //It.IsAny<DbSet<AccionAct>>()

            // Cuando el mapper reciba una entidad → devolverá un DTO de vuelta
            _mapper.Setup(m => m.Map<ActuadorDTO>(It.IsAny<Actuador>()))
                   .Returns(ActuadorDTO);


            _service = new ActuadorService(_context,_mapper.Object);

        }
        [Fact]
        public async Task CreateAsync()
        {
            var result = await _service.CreateAsync(ActuadorDTO);
            Assert.NotNull(result);
            Assert.IsType<AccionActDTO>(result);
        }
        [Fact]
        public async Task GetAllAsync()
        {
            AccionActDTO Accion = new() { IdAccionAct = Guid.NewGuid(), Accion = "Prueba" };
            var result = await _service.GetAllAsync();
            Assert.NotNull(result);
            Assert.IsType<List<AccionActDTO>>(result);
        }
        [Fact]
        public async Task UpdateAsync()
        {
            var result = await _service.UpdateAsync(Actuador.IdActuador,ActuadorDTO);
            Assert.IsType<bool>(result);
        }
        [Fact]
        public async Task DeleteAsync()
        {
            var result = await _service.DeleteAsync(Actuador.IdActuador);
            Assert.IsType<bool>(result);
        }
    }
}