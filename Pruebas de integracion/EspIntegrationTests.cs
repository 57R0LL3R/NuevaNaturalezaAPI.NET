using NuevaNaturalezaAPI.NET.Models.DTO;
using NuGet.Protocol;
using Pruebas_de_integracion;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class EspIntegrationTests : IntegrationTestBase 
{

    private readonly HttpClient client;
    public EspIntegrationTests()
    {
        client = CrearClienteAutenticado();
    }
    readonly AccionActDTO accion = new() { Accion="prueba"};
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // 🔹 Ejecutar petición GET
        var response = await client.GetAsync("/api/AccionActs");
        var data = await response.Content.ReadFromJsonAsync<List<AccionActDTO>>();
        // 🔹 Validar resultado
        response.EnsureSuccessStatusCode();
        Assert.NotNull(data);
        Assert.IsType<List<AccionActDTO>>( data);
    }
    [Fact]
    public async Task GetById()
    {
        // 🔹 Ejecutar petición GET
        var response = await client.GetAsync("/api/AccionActs/"+Guid.NewGuid().ToString());
        var data = await response.Content.ReadFromJsonAsync<AccionActDTO>();
        // 🔹 Validar resultado
        Assert.NotNull(data);
    }
    [Fact]
    public async Task AddAccion()
    {
        var nueva = new AccionActDTO { Accion = "Encender"};
        // 🔹 Ejecutar petición GET
        var response = await client.PostAsJsonAsync("/api/AccionActs", nueva );
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<AccionActDTO>();
        // 🔹 Validar resultado
        response.EnsureSuccessStatusCode();
        Assert.NotNull(data);
        Assert.IsType<AccionActDTO>(data);
    }
    [Fact]
    public async Task DeleteAccion()
    {
        // 🔹 Ejecutar petición GET
        var response = await client.DeleteAsync($"/api/AccionActs/{accion.IdAccionAct}");
        // 🔹 Validar resultado
        Assert.NotNull(response);
    }
    [Fact]
    public async Task UpdateAccion()
    {
        var nueva = new AccionActDTO { Accion = "Encender" };
        // 🔹 Ejecutar petición GET
        await client.PostAsJsonAsync("/api/AccionActs", nueva);
        nueva.Accion = "Apagar";
        // 🔹 Ejecutar petición GET
        var response = await client.PutAsJsonAsync($"/api/AccionActs/{nueva.IdAccionAct}",nueva);
        // 🔹 Validar resultado
        Assert.NotNull(response);
    }
}
