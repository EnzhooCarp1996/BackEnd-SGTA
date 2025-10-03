using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Models;
using BackEndSGTA.Data;

namespace BackEndSGTA.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly TokenService _tokenService;

    public AuthService(AppDbContext context, PasswordService passwordService, TokenService tokenService)
    {
        _context = context;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<(bool Success, string? Token, Usuario? Usuario)> LoginAsync(Login login)
    {
        // Buscar solo por nombre de usuario
        var usuario = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.NombreUsuario == login.NombreUsuario);

        if (usuario == null || !_passwordService.VerifyPassword(usuario, usuario.Contrasenia, login.Contrasenia))
            return (false, null, null);

        var token = _tokenService.GenerateToken(usuario);

        return (true, token, usuario);
    }
}
