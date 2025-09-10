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
public class PresupuestoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PresupuestoController(AppDbContext context) => _context = context;

    // GET: api/Presupuestos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Presupuesto>>> GetFactura()
    {
        // Trae todos los presupuestos
        var presupuesto = await _context.Presupuestos
                            .Include(f => f.Cliente)   // trae datos del cliente
                            .ToListAsync();
        return Ok(presupuesto);
    }

    // GET: api/Presupuestos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Presupuesto>> GetfacturaById(int id)
    {
        var presupuesto = await _context.Presupuestos
                            .Include(f => f.Cliente)
                            .FirstOrDefaultAsync(f => f.IdPresupuesto == id);

        if (presupuesto == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        return Ok(presupuesto);
    }

    // POST: api/Presupuestos
    [HttpPost]
    public async Task<ActionResult<Presupuesto>> Postfactura(Presupuesto presupuesto)
    {
        // FluentValidation se ejecuta automáticamente antes de entrar aquí
        _context.Presupuestos.Add(presupuesto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetfacturaById), new { id = presupuesto.IdPresupuesto }, presupuesto);
    }

    // PUT: api/Facturas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPresupuesto(int id, Presupuesto presupuesto)
    {
        if (id != presupuesto.IdPresupuesto)
            return BadRequest(Mensajes.MensajesPresupuestos.PRESUPUESTONOENCONTRADO);

        // Verificar que el presupuesto exista
        var presupuestoExistente = await _context.Presupuestos.FindAsync(id);

        if (presupuestoExistente == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        // Copiar los campos modificables
        _context.Entry(presupuestoExistente).CurrentValues.SetValues(presupuesto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Presupuesto/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePresupuesto(int id)
    {
        var presupuesto = await _context.Presupuestos.FindAsync(id);

        if (presupuesto == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        _context.Presupuestos.Remove(presupuesto);
        await _context.SaveChangesAsync();

        return Ok(Mensajes.MensajesPresupuestos.PRESUPUESTOELIMINADO + id);
    }
}

