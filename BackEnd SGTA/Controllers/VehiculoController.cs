using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using BackEndSGTA.Data;
using Microsoft.AspNetCore.Authorization;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiculoController : ControllerBase
{
    private readonly AppDbContext _context;

    public VehiculoController(AppDbContext context) => _context = context;

    // GET: api/Vehiculos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
    {
        // Trae todos los vehiculos
        var vehiculos = await _context.Vehiculos
                                .Include(v => v.Cliente)   // trae datos del cliente
                                .ToListAsync();
        return Ok(vehiculos);
    }

    // GET: api/Vehiculos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehiculo>> GetVehiculoById(int id)
    {
        var vehiculo = await _context.Vehiculos
                            .Include(v => v.Cliente)
                            .FirstOrDefaultAsync(v => v.IdVehiculo == id);

        if (vehiculo == null)
            return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        return Ok(vehiculo);
    }

    // POST: api/Vehiculos
    [HttpPost]
    public async Task<ActionResult<Vehiculo>> PostVehiculo([FromBody] Vehiculo vehiculo)
    {
        // FluentValidation se ejecuta automáticamente antes de entrar aquí
        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVehiculoById), new { id = vehiculo.IdVehiculo }, vehiculo);
    }

    // PUT: api/Vehiculos/5
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehiculo(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);

        if (vehiculo == null)
            return NotFound(new { mensaje = Mensajes.MensajesVehiculos.VEHICULONOTFOUND + id });

        _context.Vehiculos.Remove(vehiculo);
        await _context.SaveChangesAsync();

        return Ok(Mensajes.MensajesVehiculos.VEHICULOELIMINADO + id);
    }
}

