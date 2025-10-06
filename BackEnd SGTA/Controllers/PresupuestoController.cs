using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Services;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;

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
        var lista = await _presupuestoService.GetAllPresupuestosAsync();
        return Ok(lista);
    }

    // GET: api/Presupuesto/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Presupuesto>> GetPresupuestoById(string id)
    {
        var presupuesto = await _presupuestoService.GetByIdPresupuestoAsync(id);
        if (presupuesto == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        return Ok(presupuesto);
    }

    // POST: api/Presupuesto
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPost]
    public async Task<ActionResult<Presupuesto>> PostPresupuesto([FromBody] Presupuesto presupuesto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _presupuestoService.CreatePresupuestoAsync(presupuesto);
        return CreatedAtAction(nameof(GetPresupuestoById), new { id = presupuesto._id }, presupuesto);
    }

    // PUT: api/Presupuesto/{id}
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPresupuesto(string id, [FromBody] Presupuesto presupuesto)
    {
        var existing = await _presupuestoService.GetByIdPresupuestoAsync(id);
        if (existing == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        await _presupuestoService.UpdatePresupuestoAsync(id, presupuesto);
        return NoContent();
    }

    // DELETE: api/Presupuesto/{id}
    [Authorize(Roles = "Admin,Encargado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePresupuesto(string id)
    {
        var existing = await _presupuestoService.GetByIdPresupuestoAsync(id);
        if (existing == null)
            return NotFound(new { mensaje = Mensajes.MensajesPresupuestos.PRESUPUESTONOTFOUND + id });

        await _presupuestoService.DeletePresupuestoAsync(id);
        return NoContent();
    }
}
