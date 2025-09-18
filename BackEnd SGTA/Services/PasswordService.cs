using Microsoft.AspNetCore.Identity;
using BackEndSGTA.Models;

public class PasswordService
{
    private readonly PasswordHasher<Usuario> _hasher = new PasswordHasher<Usuario>();

    public string HashPassword(Usuario usuario, string password)
    {
        return _hasher.HashPassword(usuario, password);
    }

    public bool VerifyPassword(Usuario usuario, string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(usuario, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}
