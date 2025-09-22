using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Services;
using BackEndSGTA.Models;
using BackEndSGTA.Data;

namespace BackEndSGTA.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly PasswordService _passwordService;

    public AuthController(AppDbContext context, TokenService tokenService, PasswordService passwordService)
    {
        _context = context;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        // Buscar solo por nombre de usuario
        var usuario = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.NombreUsuario == login.NombreUsuario);

        // Verificamos que exista y que la contraseña sea correcta
        if (usuario == null || !_passwordService.VerifyPassword(usuario, usuario.Contrasenia, login.Contrasenia))
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        var token = _tokenService.GenerateToken(usuario);

        return Ok(new 
    { 
        token,
        nombreUsuario = usuario.NombreUsuario,
        role = usuario.Role.ToString()
    });
    }
}



