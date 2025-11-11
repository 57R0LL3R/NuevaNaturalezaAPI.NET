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
    public class DispositivoServiceTest

    {

        private readonly NuevaNatuContext _context;
        private readonly Mock<IMapper> _mapper;
        private readonly ISensorService sensorvc;
        private readonly IActuadorService actSrv;
        private readonly DispositivoService _service;

        readonly DispositivoDTO DispositivoDTO = new() {IdDispositivo=Guid.Parse("06e92067-9d17-4507-8e79-055587857251"), Nombre="Prueba" };
        readonly Dispositivo Dispositivo = new() { IdDispositivo = Guid.Parse("06e92067-9d17-4507-8e79-055587857251"), Nombre = "Prueba" };
        public DispositivoServiceTest()
        {
            actSrv
            var options = new DbContextOptionsBuilder<NuevaNatuContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new NuevaNatuContext(options);


            _mapper = new Mock<IMapper>();

            _mapper.Setup(m => m.Map<List<Dispositivo>>(It.IsAny<List<DispositivoDTO>>()))
                   .Returns(new List<Dispositivo>());

            // Cuando el mapper reciba una entidad → devolverá un DTO de vuelta
            _mapper.Setup(m => m.Map<List<DispositivoDTO>>(It.IsAny<List<Dispositivo>>()))
                   .Returns(new List<DispositivoDTO> { });


            _mapper.Setup(m => m.Map<Dispositivo>(It.IsAny<DispositivoDTO>()))
                   .Returns(Dispositivo); //It.IsAny<DbSet<Dispositivo>>()

            // Cuando el mapper reciba una entidad → devolverá un DTO de vuelta
            _mapper.Setup(m => m.Map<DispositivoDTO>(It.IsAny<Dispositivo>()))
                   .Returns(DispositivoDTO);

            sensorvc = new SensorService(_context, _mapper.Object,
                new TipoMUnidadMService(_context, _mapper.Object),
                new PuntoOptimoService(_context, _mapper.Object));
            actSrv = new ActuadorService(_context, _mapper.Object);
            _service = new DispositivoService(_context,_mapper.Object,sensorvc,actSrv,new ExcesoPOService(_context,_mapper.Object));

        }
        [Fact]
        public async Task CreateAsync()
        {
            var result = await _service.CreateAsync(DispositivoDTO);
            Assert.NotNull(result);
            Assert.IsType<DispositivoDTO>(result);
        }
        [Fact]
        public async Task GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            Assert.NotNull(result);
            Assert.IsType<List<DispositivoDTO>>(result);
        }
        [Fact]
        public async Task UpdateAsync()
        {
            var result = await _service.UpdateAsync(Dispositivo.IdDispositivo,DispositivoDTO);
            Assert.IsType<bool>(result);
        }
        [Fact]
        public async Task DeleteAsync()
        {
            var result = await _service.DeleteAsync(Dispositivo.IdDispositivo);
            Assert.IsType<bool>(result);
        }
    }
}