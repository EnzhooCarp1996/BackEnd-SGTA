using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Services;
using BackEndSGTA.Models;

namespace BackEndSGTA.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var (success, token, usuario) = await _authService.LoginAsync(login);

        if (!success)
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        return Ok(new 
        { 
            token,
            nombreUsuario = usuario!.NombreUsuario,
            role = usuario!.Role.ToString()
        });
    }
}
