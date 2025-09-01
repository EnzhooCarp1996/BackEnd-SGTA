using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEndSGTA.Models;

public class Vehiculo
{
    public int IdVehiculo { get; set; }
    public string Patente { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Anio { get; set; }
    public string NroDeChasis { get; set; } = string.Empty;
    public TipoEstado Estado { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FechaRecibido { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FechaEsperada { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FechaEntrega { get; set; }
    public string DescripcionTrabajos { get; set; } = string.Empty;
    public int IdCliente { get; set; }
    [ForeignKey("IdCliente")]
    public required Cliente Cliente { get; set; }

    public enum TipoEstado
    {
        Recibido,
        NoRecibido,
        Proceso,
        Entregado
    }



}
/*
    id_vehiculo INT AUTO_INCREMENT PRIMARY KEY,
    patente VARCHAR(20) NOT NULL UNIQUE,
    marca VARCHAR(50) NOT NULL,
    modelo VARCHAR(50) NOT NULL,
    anio YEAR NOT NULL,
    nro_de_Chasis VARCHAR(50) NOT NULL UNIQUE,
    estado ENUM('Recibido', 'No Recibido', 'Proceso', 'Entregado') NOT NULL DEFAULT 'No Recibido',
    fecha_recibido DATE,
    fecha_esperada DATE,
    fecha_entrega DATE,
    descripcion_trabajos TEXT,
    id_cliente INT NOT NULL
*/