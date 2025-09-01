
namespace BackEndSGTA.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Persona : Cliente
{
    [Required]
    [StringLength(100)]
    public required string Nombre { get; set; }

    [Required]
    [StringLength(100)]
    public required string Apellido { get; set; }

}



