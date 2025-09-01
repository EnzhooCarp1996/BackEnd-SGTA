using BackEndSGTA.Data;
using BackEndSGTA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiculoControllers : ControllerBase
{
    private readonly AppDbContext _context;

    public VehiculoControllers(AppDbContext context)
    {
        _context = context;
    }

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
            return NotFound();

        return Ok(vehiculo);
    }

    // POST: api/Vehiculos
    [HttpPost]
    public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
    {
        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVehiculoById), new { id = vehiculo.IdVehiculo }, vehiculo);
    }

    // PUT: api/Vehiculos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
    {
        if (id != vehiculo.IdVehiculo)
            return BadRequest();

        _context.Entry(vehiculo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Vehiculos.Any(e => e.IdVehiculo == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/Vehiculos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehiculo(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null)
            return NotFound();

        _context.Vehiculos.Remove(vehiculo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

