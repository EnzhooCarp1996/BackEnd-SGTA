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
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context) => _context = context;

    // GET: api/Cliente
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        // Trae todos los clientes incluyendo Persona o Empresa
        var clientes = await _context.Clientes
            .Include(c => c.Vehiculos)
            .Include(c => c.Presupuestos)
            .AsSplitQuery()
            .ToListAsync();

        return Ok(clientes);
    }

    // GET: api/Cliente/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetClienteById(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Vehiculos)
            .Include(c => c.Presupuestos)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.IdCliente == id);

        if (cliente == null)
            return NotFound(new { mensaje = Mensajes.MensajesClientes.CLIENTENOTFOUND + id });

        return Ok(cliente);
    }

    // POST: api/Cliente
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
    {
        // FluentValidation se ejecuta automáticamente antes de entrar aquí
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClienteById), new { id = cliente.IdCliente }, cliente);
    }


    // PUT: api/cliente/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
    {
        if (id != cliente.IdCliente)
            return BadRequest(Mensajes.MensajesClientes.CLIENTENOENCONTRADO);

        // Verificar que el cliente exista
        var clienteExistente = await _context.Clientes.FindAsync(id);

        if (clienteExistente == null)
            return NotFound(new { mensaje = Mensajes.MensajesClientes.CLIENTENOTFOUND + id });

        // Copiar los campos modificables
        _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);

        await _context.SaveChangesAsync();

        return NoContent();
    }



    // DELETE: api/Cliente/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return NotFound(new { mensaje = Mensajes.MensajesClientes.CLIENTENOTFOUND + id });

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return Ok(Mensajes.MensajesClientes.CLIENTEELIMINADO + id);
    }


}
