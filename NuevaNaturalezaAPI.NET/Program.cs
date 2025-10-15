using Microsoft.EntityFrameworkCore;
using NuevaNaturalezaAPI.NET.Models.DB;
using NuevaNaturalezaAPI.NET.Services.Implementations;
using NuevaNaturalezaAPI.NET.Services.Interfaces;
using NuevaNaturalezaAPI.NET.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecreta12345"; // puedes ponerlo en appsettings.json
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "https://localhost:44330";

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
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
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IExcesoPOService, ExcesoPOService>();
builder.Services.AddScoped<ITipoExcesoService, TipoExcesoService>();
builder.Services.AddScoped<ISugerenciaService, SugerenciaService>();
builder.Services.AddScoped<IChecklistService, ChecklistService>();
builder.Services.AddScoped<IProgramacionDosificadorService, ProgramacionDosificadorService>();
builder.Services.AddScoped<IDosificadorService, DosificadorService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    // Permitir JWT en cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("jwt"))
                context.Token = context.Request.Cookies["jwt"];
            return Task.CompletedTask;
        }
    };
});

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

app.UseHttpsRedirection();
app.UseCors(allowAll);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificacionesHub>("/hubs/notificaciones");
app.Run();
