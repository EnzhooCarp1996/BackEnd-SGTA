namespace BackEndSGTA.Models;

public class Vehiculo
{
    public int IdVehiculo { get; set; }
    public required string Patente { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public int Anio { get; set; }
    public required string NroDeChasis { get; set; }
    public string? Estado { get; set; }
    public DateTime? FechaRecibido { get; set; }
    public DateTime? FechaEsperada { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string? DescripcionTrabajos { get; set; }

    public int IdCliente { get; set; }
    public required Cliente Cliente { get; set; }
}
