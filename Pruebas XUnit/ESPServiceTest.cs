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
    public class ESPServiceTest

    {

        private readonly NuevaNatuContext _context;
        private readonly Mock<IMapper> _mapper;
        private readonly AccionActService _service;

        readonly AccionActDTO AccionDTO = new() {IdAccionAct=Guid.Parse("06e92067-9d17-4507-8e79-055587857251"), Accion="Prueba" };
        readonly AccionAct Accion = new() { IdAccionAct = Guid.Parse("06e92067-9d17-4507-8e79-055587857251"), Accion = "Prueba" };
        public ESPServiceTest()
        {
            // Creamos el repositorio falso
            /*_repoMock = new Mock<NuevaNatuContext>();

            // Inyectamos el mock al servicio
            var mockSet = new Mock<DbSet<AccionAct>>();

            _repoMock.Setup(c => c.Entry(It.IsAny<AccionAct>())).Returns(It.IsAny<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AccionAct>>());

            _repoMock.Setup(c => c.AccionAct).Returns(mockSet.Object);
            _repoMock.Setup(c => c.AccionAct.FindAsync(It.IsAny<Guid>())).ReturnsAsync(new AccionAct());
            */


            var options = new DbContextOptionsBuilder<NuevaNatuContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new NuevaNatuContext(options);


            _mapper = new Mock<IMapper>();

            _mapper.Setup(m => m.Map<List<AccionAct>>(It.IsAny<List<AccionActDTO>>()))
                   .Returns(new List<AccionAct>());

            // Cuando el mapper reciba una entidad → devolverá un DTO de vuelta
            _mapper.Setup(m => m.Map<List<AccionActDTO>>(It.IsAny<List<AccionAct>>()))
                   .Returns(new List<AccionActDTO> { });


            _mapper.Setup(m => m.Map<AccionAct>(It.IsAny<AccionActDTO>()))
                   .Returns(Accion); //It.IsAny<DbSet<AccionAct>>()

            // Cuando el mapper reciba una entidad → devolverá un DTO de vuelta
            _mapper.Setup(m => m.Map<AccionActDTO>(It.IsAny<AccionAct>()))
                   .Returns(AccionDTO);


            _service = new AccionActService(_context,_mapper.Object);

        }
        [Fact]
        public async Task CreateAsync()
        {
            var result = await _service.CreateAsync(AccionDTO);
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
            var result = await _service.UpdateAsync(Accion.IdAccionAct,AccionDTO);
            Assert.IsType<bool>(result);
        }
        [Fact]
        public async Task DeleteAsync()
        {
            var result = await _service.DeleteAsync(Accion.IdAccionAct);
            Assert.IsType<bool>(result);
        }
    }
}