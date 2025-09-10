
namespace BackEndSGTA.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public required string NombreUsuario { get; set; }
    public string? Correo { get; set; }
    public required string Contrasenia { get; set; }
    public RolUsuario Rol { get; set; }

    public enum RolUsuario
    {
        Admin,
        Encargado,
        Empleado
    }

}
