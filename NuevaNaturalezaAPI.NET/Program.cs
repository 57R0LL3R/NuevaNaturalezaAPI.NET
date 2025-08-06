using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NuevaNatuContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IAuthService, AuthService>();
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
builder.Services.AddScoped<ITipoDispositivoService, TipoDispositivoService>();


string allowAll = "allowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAll, policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
var app = builder.Build();


app.UseCors(allowAll);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
