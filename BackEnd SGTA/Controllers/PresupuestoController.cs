using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using BackEndSGTA.Services;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PresupuestoController : ControllerBase
{
    private readonly PresupuestoService _presupuestoService;

    public PresupuestoController(PresupuestoService presupuestoService)
    {
        _presupuestoService = presupuestoService;
    }

    // GET: api/Presupuesto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Presupuesto>>> GetPresupuestos()
    {
        var lista = await _presupuestoService.GetAllAsync();
        return Ok(lista);
    }

    // GET: api/Presupuesto/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Presupuesto>> GetPresupuestoById(string id)
    {
        var presupuesto = await _presupuestoService.GetByIdAsync(id);
        if (presupuesto == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        return Ok(presupuesto);
    }

    // POST: api/Presupuesto
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPost]
    public async Task<ActionResult<Presupuesto>> PostPresupuesto([FromBody] Presupuesto presupuesto)
    {
        await _presupuestoService.CreateAsync(presupuesto);
        return CreatedAtAction(nameof(GetPresupuestoById), new { id = presupuesto.IdPresupuesto }, presupuesto);
    }

    // PUT: api/Presupuesto/{id}
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPresupuesto(string id, [FromBody] Presupuesto presupuesto)
    {
        var existing = await _presupuestoService.GetByIdAsync(id);
        if (existing == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        await _presupuestoService.UpdateAsync(id, presupuesto);
        return NoContent();
    }

    // DELETE: api/Presupuesto/{id}
    [Authorize(Roles = "Admin,Encargado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePresupuesto(string id)
    {
        var existing = await _presupuestoService.GetByIdAsync(id);
        if (existing == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        await _presupuestoService.DeleteAsync(id);
        return NoContent();
    }
}
