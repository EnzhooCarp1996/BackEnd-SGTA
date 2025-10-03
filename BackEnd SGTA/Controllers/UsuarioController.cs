using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using Microsoft.AspNetCore.Authorization;
using BackEndSGTA.Services;

namespace BackEndSGTA.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // GET: api/Usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var usuarios = await _usuarioService.GetUsuariosAsync();
        return Ok(usuarios);
    }

    // GET: api/Usuario/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
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
        var nuevoUsuario = await _usuarioService.CreateAsync(usuario);
        return CreatedAtAction(nameof(GetUsuarioById), new { id = nuevoUsuario.IdUsuario }, nuevoUsuario);
    }

    // PUT: api/Usuario/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        var actualizado = await _usuarioService.UpdateUsuarioAsync(id, usuario);
        if (!actualizado) return NotFound(new { mensaje = Mensajes.MensajesUsuarios.USUARIONOTFOUND + id });

        return NoContent();
    }

    // DELETE: api/Usuario/5
    [Authorize(Roles = "Admin,Encargado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var eliminado = await _usuarioService.DeleteUsuarioAsync(id);
        if (!eliminado) return NotFound(new { mensaje = Mensajes.MensajesUsuarios.USUARIONOTFOUND + id });

        return NoContent();
    }
}
