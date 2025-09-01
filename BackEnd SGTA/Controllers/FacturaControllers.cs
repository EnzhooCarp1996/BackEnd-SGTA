using BackEndSGTA.Data;
using BackEndSGTA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacturaControllers : ControllerBase
{
    private readonly AppDbContext _context;

    public FacturaControllers(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Facturas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Factura>>> GetFactura()
    {
        // Trae todos los factura incluyendo Persona o Empresa
        var factura = await _context.Facturas
                            .Include(f => f.Cliente)   // incluye cliente
                            .ToListAsync();
        return Ok(factura);
    }

    // GET: api/Facturas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Factura>> GetfacturaById(int id)
    {
        var factura = await _context.Facturas
                            .Include(f => f.Cliente)
                            .FirstOrDefaultAsync(f => f.IdFactura == id);
                            
        if (factura == null)
            return NotFound();

        return Ok(factura);
    }

    // POST: api/Facturas
    [HttpPost]
    public async Task<ActionResult<Factura>> Postfactura(Factura factura)
    {
        _context.Facturas.Add(factura);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetfacturaById), new { id = factura.IdFactura }, factura);
    }

    // PUT: api/Facturas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFactura(int id, Factura factura)
    {
        if (id != factura.IdFactura)
            return BadRequest();

        _context.Entry(factura).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Facturas.Any(e => e.IdFactura == id))
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

