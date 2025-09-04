using System.ComponentModel.DataAnnotations;
using BackEndSGTA.Helpers;

namespace BackEndSGTA.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    [Required]
    [StringLength(Mensajes.MAXTREINTA)]
    public required string NombreUsuario { get; set; }

    [Required]
    [StringLength(Mensajes.MAX150)]
    [EmailAddress(ErrorMessage = Mensajes.CORREOINVALIDO)]
    public required string Correo { get; set; }

    [Required]
    [StringLength(Mensajes.MAXTREINTA)]
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
