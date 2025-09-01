using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndSGTA.Models;

public class Empresa : Cliente
{
    [Required]
    [StringLength(150)]
    public required string RazonSocial { get; set; }

    [StringLength(150)]
    public string? NombreDeFantasia { get; set; }

}
