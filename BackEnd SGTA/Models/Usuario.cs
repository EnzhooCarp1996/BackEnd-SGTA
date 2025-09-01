using System.ComponentModel.DataAnnotations;

namespace BackEndSGTA.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    [Required]
    [StringLength(100)]
    public required string NombreUsuario { get; set; }

    [Required]
    [StringLength(150)]
    [EmailAddress(ErrorMessage = "Correo no válido.")]
    public required string Correo { get; set; }

    [Required]
    [StringLength(255)]
    public required string Contrasenia { get; set; }

    [Required]
    public TipoUsuario Tipo { get; set; }

    public enum TipoUsuario
    {
        Admin,
        Encargado,
        Empleado
    }

}
