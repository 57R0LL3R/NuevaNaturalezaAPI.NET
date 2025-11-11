using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NuevaNaturalezaAPI.NET.Models.DB;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Pruebas_de_integracion
{
    public class IntegrationTestBase : WebApplicationFactory<Program>
    {
        private IServiceProvider? _serviceProvider;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true)
                      .AddEnvironmentVariables();
            });

            builder.ConfigureServices(services =>
            {
                // 🔹 1. Eliminar cualquier configuración previa del DbContext
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<NuevaNatuContext>)
                );
                if (descriptor != null)
                    services.Remove(descriptor);

                // 🔹 2. Registrar el contexto con base de datos en memoria
                services.AddDbContext<NuevaNatuContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb")
                           .UseInternalServiceProvider(new ServiceCollection()
                               .AddEntityFrameworkInMemoryDatabase()
                               .BuildServiceProvider());
                });

                // 🔹 3. Construir el nuevo ServiceProvider *solo para las pruebas*
                _serviceProvider = services.BuildServiceProvider();

                // 🔹 4. Crear el contexto y asegurar la base de datos
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<NuevaNatuContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("🧪 Base de datos en memoria lista para pruebas");
            });
        }

        /// <summary>
        /// 🔹 Genera un JWT válido basado en las claves configuradas en appsettings.json o variables de entorno.
        /// </summary>
        protected string GenerarJwtFalsoDesdeConfig()
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("El ServiceProvider aún no ha sido inicializado.");

            var config = _serviceProvider.GetRequiredService<IConfiguration>();
            var key = config["Jwt:Key"] ?? "ClaveSuperSecretaDePrueba123!";
            var issuer = config["Jwt:Issuer"] ?? "TestIssuer";
            var audience = config["Jwt:Audience"] ?? "TestAudience";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "UsuarioDePrueba"),
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("scope", "api_access")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 🔹 Crea un cliente HTTP autenticado con un JWT válido.
        /// </summary>
        protected HttpClient CrearClienteAutenticado()
        {
            var client = CreateClient();
            var token = GenerarJwtFalsoDesdeConfig();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
