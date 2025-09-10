namespace BackEndSGTA.Models;

public class Login
{
    public required string NombreUsuario { get; set; }
    public string? Correo { get; set; }
    public required string Contrasenia { get; set; }


}