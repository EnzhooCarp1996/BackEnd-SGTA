using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using BackEndSGTA.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using BackEndSGTA.Services;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _vehiculoService;
    private readonly HttpClient _httpClient;

    public VehiculoController(VehiculoService vehiculoService, IHttpClientFactory httpClientFactory)
    {
        _vehiculoService = vehiculoService;
        _httpClient = httpClientFactory.CreateClient();
    }

    // GET: api/Vehiculos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
    {
        var vehiculos = await _vehiculoService.GetVehiculoAllAsync();
        return Ok(vehiculos);
    }

    // GET: api/Vehiculos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetVehiculoById(int id)
    {
        var vehiculo = await _vehiculoService.GetVehiculoByIdAsync(id);
        if (vehiculo == null)
            return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        return Ok(vehiculo);
    }

    // POST: api/Vehiculos
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPost]
    public async Task<ActionResult<Vehiculo>> PostVehiculo([FromBody] Vehiculo vehiculo)
    {
        var created = await _vehiculoService.CreateVehiculoAsync(vehiculo);
        return CreatedAtAction(nameof(GetVehiculoById), new { id = created.IdVehiculo }, created);
    }

    // PUT: api/Vehiculos/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehiculo(int id, [FromBody] Vehiculo vehiculo)
    {
        var updated = await _vehiculoService.UpdateVehiculoAsync(id, vehiculo);
        if (!updated) return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        return NoContent();
    }

    // DELETE: api/Vehiculo/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehiculo(int id)
    {
        var deleted = await _vehiculoService.DeleteVehiculoAsync(id);
        if (!deleted) return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        return NoContent();
    }


    //----------------------------------------------------------------------------------------------------------------

    // GET: api/Vehiculo/marcas
    [HttpGet("marcas")]
    public async Task<IActionResult> GetMarcas()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://www.carqueryapi.com/api/0.3/?cmd=getMakes");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "No se pudieron cargar las marcas", error = ex.Message });
        }
    }

    // GET: api/Vehiculo/modelos/{marca}
    [HttpGet("modelos/{marca}")]
    public async Task<IActionResult> GetModelos(string marca)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://www.carqueryapi.com/api/0.3/?cmd=getModels&make={marca}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "No se pudieron cargar los modelos", error = ex.Message });
        }
    }
}

