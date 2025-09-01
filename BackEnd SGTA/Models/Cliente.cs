using System.ComponentModel.DataAnnotations;

namespace BackEndSGTA.Models;

public abstract class Cliente
{
    public int IdCliente { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Solo debe contener números.")]
    [StringLength(15, ErrorMessage = "No puede tener más de 15 dígitos.")]
    public string? Telefono { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Solo debe contener números.")]
    [StringLength(15, ErrorMessage = "No puede tener más de 15 dígitos.")]
    public string? Celular { get; set; }

    [Required]
    public required TipoResponsabilidad Responsabilidad { get; set; }

    [Required]
    public required TipoDocumento Tipo { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Solo debe contener números.")]
    [StringLength(20, ErrorMessage = "No puede tener más de 20 dígitos.")]
    [Required]
    public required string Documento { get; set; }

    [Required]
    public TipoDeCliente TipoCliente { get; set; }

    public required ICollection<Vehiculo> Vehiculos { get; set; }
    public ICollection<Factura>? Facturas { get; set; }

    public enum TipoDeCliente
    {
        Persona,
        Empresa
    }


    public enum TipoDocumento
    {
        DNI,
        CUIL,
        CUIT
    }

    public enum TipoResponsabilidad
    {
        ConsumidorFinal,
        Monotributista,
        ResponsableInscripto
    }



}


/*
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    tipo_cliente ENUM('Persona', 'Empresa') NOT NULL,  -- Discriminator
    telefono VARCHAR(20),
    celular VARCHAR(20),
    responsabilidad ENUM('Consumidor final', 'Monotributista', 'Responsable inscripto') NOT NULL,
    tipo_documento ENUM('DNI', 'CUIL', 'CUIT'),
    documento VARCHAR(20) UNIQUE,
    nombre VARCHAR(100),           -- solo Persona
    apellido VARCHAR(100),         -- solo Persona
    razon_social VARCHAR(150),     -- solo Empresa
    nombre_de_fantasia VARCHAR(150) -- solo Empresa
*/