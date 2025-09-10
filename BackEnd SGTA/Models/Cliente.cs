
namespace BackEndSGTA.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string? Telefono { get; set; }
    public string? Celular { get; set; }
    public required TipoResponsabilidad Responsabilidad { get; set; }
    public required TipoDeDocumento TipoDocumento { get; set; }
    public required string Documento { get; set; }
    public TipoDeCliente TipoCliente { get; set; }
    // Campos específicos de Persona
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    // Campos específicos de Empresa
    public string? RazonSocial { get; set; }
    public string? NombreDeFantasia { get; set; }
    public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    public ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();

    public enum TipoDeCliente
    {
        Persona,
        Empresa
    }

    public enum TipoDeDocumento
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