using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using BackEndSGTA.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiculoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public VehiculoController(AppDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
    }

    // GET: api/Vehiculos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
    {
        // Trae todos los vehiculos
        var vehiculos = await _context.Vehiculos.ToListAsync();
        return Ok(vehiculos);
    }

    // GET: api/Vehiculos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetVehiculoById(int id)
    {
        var vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(v => v.IdVehiculo == id);

        if (vehiculo == null)
            return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        return Ok(vehiculo);
    }

    // POST: api/Vehiculos
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPost]
    public async Task<ActionResult<Vehiculo>> PostVehiculo([FromBody] Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // esto devuelve los errores exactos de validación
        }
        // FluentValidation se ejecuta automáticamente antes de entrar aquí
        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVehiculoById), new { id = vehiculo.IdVehiculo }, vehiculo);
    }

    // PUT: api/Vehiculos/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehiculo(int id, [FromBody] Vehiculo vehiculo)
    {
        if (id != vehiculo.IdVehiculo)
            return BadRequest(Mensajes.MensajesVehiculos.VEHICULONOENCONTRADO);

        // Verificar que el vehiculo exista
        var vehiculoExistente = await _context.Vehiculos.FindAsync(id);

        if (vehiculoExistente == null)
            return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        // Copiar los campos modificables
        _context.Entry(vehiculoExistente).CurrentValues.SetValues(vehiculo);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Vehiculo/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehiculo(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);

        if (vehiculo == null)
            return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        _context.Vehiculos.Remove(vehiculo);
        await _context.SaveChangesAsync();

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

