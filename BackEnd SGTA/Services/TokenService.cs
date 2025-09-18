using BackEndSGTA.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackEndSGTA.Services;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(Usuario usuario)
    {
        var jwt = _config.GetSection("Jwt").Get<Jwt>();

        if (jwt == null)
        {
            throw new InvalidOperationException("El objeto JWT no puede ser nulo.");
        }

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                            new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim("NombreUsuario", usuario.NombreUsuario ?? string.Empty),
                new Claim("Contrasenia", usuario.Contrasenia ?? string.Empty),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt.Key ?? throw new InvalidOperationException("JWT Key not configured")));

        var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: singIn
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string EncriptarSHA256(string texto)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Computar el hash
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            //Convertir el array de bytes a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}

