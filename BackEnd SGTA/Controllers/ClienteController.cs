using BackEndSGTA.Data;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BackEndSGTA.Models.Cliente;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

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
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Vehiculos)
            .Include(c => c.Presupuestos)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.IdCliente == id);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    // POST: api/Cliente
    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
    {
        var error = ValidarCliente(cliente);
        if (error != null)
            return BadRequest(error);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
    }


    // PUT: api/Cliente/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
    {
        // Asignar el Id de la URL al objeto recibido
        cliente.IdCliente = id;

        // Validación de existencia
        var clienteExistente = await _context.Clientes.FindAsync(id);
        if (clienteExistente == null)
            return NotFound(Mensajes.MensajesClientes.CLIENTENOTFOUND + id);

        // Validación de campos
        var error = ValidarCliente(cliente, esActualizacion: true);
        if (error != null)
            return BadRequest(error);

        // Actualizar los campos
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
            return NotFound(Mensajes.MensajesClientes.CLIENTENOTFOUND + id);

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return Ok(Mensajes.MensajesClientes.CLIENTEOK + id);
    }



    // Método auxiliar de validación
    private string? ValidarCliente(Cliente cliente, bool esActualizacion = false)
    {
        // Validar persona vs empresa
        if (cliente.TipoCliente == TipoDeCliente.Persona)
        {
            if (!string.IsNullOrEmpty(cliente.RazonSocial) || !string.IsNullOrEmpty(cliente.NombreDeFantasia))
                return Mensajes.MensajesClientes.VALIDARPERSONA;

            if (cliente.TipoDocumento != TipoDeDocumento.DNI)
                return Mensajes.MensajesClientes.VALIDARDNI;
        }
        else // Empresa o Monotributista
        {
            if (!string.IsNullOrEmpty(cliente.Nombre) || !string.IsNullOrEmpty(cliente.Apellido))
                return Mensajes.MensajesClientes.VALIDAREMPRESA;

            if (cliente.TipoDocumento != TipoDeDocumento.CUIT)
                return Mensajes.MensajesClientes.VALIDAREMPRESA;
        }

        // Validar duplicados en documento
        bool existe;
        if (esActualizacion)
        {
            // Ignorar el mismo cliente en PUT
            existe = _context.Clientes.Any(c => c.Documento == cliente.Documento && c.IdCliente != cliente.IdCliente);
        }
        else
        {
            existe = _context.Clientes.Any(c => c.Documento == cliente.Documento);
        }

        if (existe)
            return Mensajes.MensajesClientes.CLIENTEREPETIDO;

        // Todo ok
        return null;
    }


    // Métodos de chequeo de formato (pueden ser regex más completos)
    private bool EsDNI(string documento) => documento.All(char.IsDigit) && documento.Length == 8;

    private bool EsCUIT(string documento) => documento.Replace("-", "").All(char.IsDigit) && documento.Length == 11;
}
