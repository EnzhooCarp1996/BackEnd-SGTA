using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BackEndSGTA.Models;

public class Presupuesto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public int? IdCliente { get; set; }
    public required string Cliente { get; set; }
    public string? Domicilio { get; set; }
    public string? Poliza { get; set; }
    public int? IdVehiculo { get; set; }
    public required string Vehiculo { get; set; }
    public string? Patente { get; set; }
    public string? Siniestro { get; set; }
    public decimal ManoDeObraChapa { get; set; }
    public decimal ManoDeObraPintura { get; set; }
    public decimal Mecanica { get; set; }
    public decimal Electricidad { get; set; }
    public decimal TotalRepuestos { get; set; }
    public string? LugarFecha { get; set; }
    public string? FirmaCliente { get; set; }
    public string? FirmaResponsable { get; set; }
    public string? Observaciones { get; set; }
    public string? RuedaAuxilio { get; set; }
    public string? Encendedor { get; set; }
    public string? Cricket { get; set; }
    public string? Herramientas { get; set; }
    public decimal Total { get; set; }
    public List<PresupuestoItem> Items { get; set; } = new();
}

[BsonIgnoreExtraElements]
public class PresupuestoItem
{
    public int? Id { get; set; }          // opcional, si los generás en front
    public string? Ubicacion { get; set; }
    public string? Descripcion { get; set; }
    public string? A { get; set; }
    public string? B { get; set; }
    public string? Observaciones { get; set; }
    public decimal Importe { get; set; }
}
