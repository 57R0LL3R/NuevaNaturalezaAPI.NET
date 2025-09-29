using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext<NuevaNatuContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccionActService, AccionActService>();
builder.Services.AddScoped<IActuadorService, ActuadorService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();
builder.Services.AddScoped<IDispositivoService, DispositivoService>();
builder.Services.AddScoped<IEstadoDispositivoService, EstadoDispositivoService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IFechaMedicionService, FechaMedicionService>();
builder.Services.AddScoped<IImpactoService, ImpactoService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IMedicionService, MedicionService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IPuntoOptimoService, PuntoOptimoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISistemaService, SistemaService>();
builder.Services.AddScoped<ITipoDispositivoService, TipoDispositivoService>(); 
builder.Services.AddScoped<ITituloService,TituloService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITipoMedicionService, TipoMedicionService>();
builder.Services.AddScoped<ITipoMUnidadMService, TipoMUnidadMService>();
builder.Services.AddScoped<ITipoNotificacionService, TipoNotificacionService>();
builder.Services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
builder.Services.AddScoped<IESPService, ESPService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISugerenciaService, SugerenciaService>();


string allowAll = "allowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAll, policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // tu frontend Angular
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // 👈 esto es clave
    });       
});
var app = builder.Build();


app.UseCors(allowAll);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<NotificacionesHub>("/hubs/notificaciones");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
