using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Services;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _vehiculoService;

    public VehiculoController(VehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }

    // GET: api/Vehiculos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
    {
        var vehiculos = await _vehiculoService.GetAllVehiculosAsync();
        return Ok(vehiculos);
    }

    // GET: api/Vehiculos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetVehiculoById(int id)
    {
        var vehiculo = await _vehiculoService.GetByIdVehiculoAsync(id);
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
            var marcas = await _vehiculoService.GetDistinctMarcasAsync();
            return Ok(marcas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al obtener las marcas", error = ex.Message });
        }
    }

    // GET: api/Vehiculo/modelos/{marca}
    [HttpGet("modelos/{marca}")]
    public async Task<IActionResult> GetModelos(string marca)
    {
        try
        {
            var modelos = await _vehiculoService.GetModelosByMarcaAsync(marca);
            return Ok(modelos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al obtener los modelos", error = ex.Message });
        }
    }
}

