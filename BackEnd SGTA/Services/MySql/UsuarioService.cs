using BackEndSGTA.Data;
using BackEndSGTA.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndSGTA.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwordService;

    public UsuarioService(AppDbContext context, PasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task<List<Usuario>> GetUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        var registroUsuario = new Usuario
        {
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            Contrasenia = _passwordService.HashPassword(usuario, usuario.Contrasenia),
            Role = usuario.Role
        };

        await _context.Usuarios.AddAsync(registroUsuario);
        await _context.SaveChangesAsync();

        return registroUsuario;
    }

    public async Task<bool> UpdateUsuarioAsync(int id, Usuario usuario)
    {
        var usuarioExistente = await _context.Usuarios.FindAsync(id);
        if (usuarioExistente == null) return false;

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
        return true;
    }

    public async Task<bool> DeleteUsuarioAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
