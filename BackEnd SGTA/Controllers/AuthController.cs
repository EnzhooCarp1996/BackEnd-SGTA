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

    public AuthController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var usuario = await _context.Usuarios
                        .Where(u =>
                            u.NombreUsuario == login.NombreUsuario &&
                            u.Contrasenia == _tokenService.EncriptarSHA256(login.Contrasenia))
                        .FirstOrDefaultAsync();

        if (usuario == null)
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        var token = _tokenService.GenerateToken(usuario);

        return Ok(new { token });
    }
}



