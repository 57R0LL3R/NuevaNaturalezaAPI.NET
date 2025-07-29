using NuevaNaturalezaAPI.NET.Models;
using NuevaNaturalezaAPI.NET.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NuevaNatuContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Puedes pasar el assembly también

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
