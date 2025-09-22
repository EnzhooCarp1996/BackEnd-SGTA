using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using BackEndSGTA.Data;
using Microsoft.AspNetCore.Authorization;
using BackEndSGTA.Services;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly PasswordService _passwordService;


    public UsuarioController(AppDbContext context, TokenService tokenService, PasswordService passwordService)
    {
        _context = context;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }

    // GET: api/Usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        // Trae todos los usuarios
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    // GET: api/Usuario/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound(new { mensaje = Mensajes.MensajesUsuarios.USUARIONOTFOUND + id });

        return Ok(usuario);
    }

    // POST: api/Usuario
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        // FluentValidation se ejecuta automáticamente antes de entrar aquí
        var registroUsuario = new Usuario
        {
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            Contrasenia = _passwordService.HashPassword(usuario, usuario.Contrasenia),
            Role = usuario.Role
        };

        await _context.Usuarios.AddAsync(registroUsuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarioById), new { id = registroUsuario.IdUsuario }, registroUsuario);
    }

    // PUT: api/Usuarios/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.IdUsuario)
            return BadRequest(Mensajes.MensajesUsuarios.USUARIONOENCONTRADO);

        // Verificar que el usuario exista
        var usuarioExistente = await _context.Usuarios.FindAsync(id);

        if (usuarioExistente == null)
            return NotFound(new { mensaje = Mensajes.MensajesUsuarios.USUARIONOTFOUND + id });

        // Actualizar campos modificables
        usuarioExistente.NombreUsuario = usuario.NombreUsuario;
        usuarioExistente.Correo = usuario.Correo;
        usuarioExistente.Role = usuario.Role;

        // Solo si se envía nueva contraseña
        if (!string.IsNullOrEmpty(usuario.Contrasenia))
        {
            usuarioExistente.Contrasenia = _passwordService.HashPassword(usuarioExistente, usuario.Contrasenia);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Usuarios/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound(new { mensaje = Mensajes.MensajesUsuarios.USUARIONOTFOUND + id });

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}

