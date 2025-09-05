using BackEndSGTA.Data;
using BackEndSGTA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacturaController : ControllerBase
{
    private readonly AppDbContext _context;

    public FacturaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Facturas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Presupuesto>>> GetFactura()
    {
        // Trae todos los factura incluyendo Persona o Empresa
        var factura = await _context.Facturas
                            .Include(f => f.Cliente)   // incluye cliente
                            .ToListAsync();
        return Ok(factura);
    }

    // GET: api/Facturas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Presupuesto>> GetfacturaById(int id)
    {
        var factura = await _context.Facturas
                            .Include(f => f.Cliente)
                            .FirstOrDefaultAsync(f => f.IdPresupuesto == id);

        if (factura == null)
            return NotFound();

        return Ok(factura);
    }

    // POST: api/Facturas
    [HttpPost]
    public async Task<ActionResult<Presupuesto>> Postfactura(Presupuesto factura)
    {
        _context.Facturas.Add(factura);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetfacturaById), new { id = factura.IdPresupuesto }, factura);
    }

    // PUT: api/Facturas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFactura(int id, Presupuesto factura)
    {
        if (id != factura.IdPresupuesto)
            return BadRequest();

        _context.Entry(factura).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Facturas.Any(e => e.IdPresupuesto == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/Facturas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFactura(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura == null)
            return NotFound();

        _context.Facturas.Remove(factura);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

