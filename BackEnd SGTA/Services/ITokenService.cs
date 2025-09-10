using BackEndSGTA.Models;

namespace BackEndSGTA.Services;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}

