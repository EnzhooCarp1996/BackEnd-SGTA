using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Services;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClienteController : ControllerBase
{
    private readonly ClienteService _service;

    public ClienteController(ClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        var clientes = await _service.GetClienteAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetClienteById(int id)
    {
        var cliente = await _service.GetClienteByIdAsync(id);
        if (cliente == null)
            return NotFound(new { mensaje = Mensajes.MensajesClientes.CLIENTENOTFOUND + id });

        return Ok(cliente);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Encargado")]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
    {
        var creado = await _service.CreateAsync(cliente);
        return CreatedAtAction(nameof(GetClienteById), new { id = creado.IdCliente }, creado);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Encargado")]
    public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
    {
        try
        {
            await _service.UpdateClienteAsync(id, cliente);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { mensaje = Mensajes.MensajesClientes.CLIENTENOTFOUND + id });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Encargado")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        try
        {
            await _service.DeleteClienteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { mensaje = Mensajes.MensajesClientes.CLIENTENOTFOUND + id });
        }
    }
}
