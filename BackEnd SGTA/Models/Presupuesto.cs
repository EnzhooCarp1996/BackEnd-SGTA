using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BackEndSGTA.Models;

public class Presupuesto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdPresupuesto { get; set; } = null!; // mongo genera el _id

    public DateTime? Fecha { get; set; }
    public int? IdCliente { get; set; }
    public string? Nombre { get; set; }
    public string? Domicilio { get; set; }
    public string? Poliza { get; set; }
    public int? IdVehiculo { get; set; }
    public string? Descripcion { get; set; }
    public string? Patente { get; set; }
    public string? Siniestro { get; set; }
    public decimal Chapa { get; set; }
    public decimal Pintura { get; set; }
    public decimal Mecanica { get; set; }
    public decimal Electricidad { get; set; }
    public decimal Repuestos { get; set; }
    public decimal Total { get; set; }
    public List<ItemUbicacion> Items { get; set; } = new();
    public string? FirmaCliente { get; set; }
    public string? FirmaResponsable { get; set; }
    public string? LugarFecha { get; set; }
    public string? Observaciones { get; set; }
    public string? RuedaAuxilio { get; set; }
    public string? Encendedor { get; set; }
    public string? Cricket { get; set; }
    public string? Herramientas { get; set; }
}

public class ItemUbicacion
{
    public string Ubicacion { get; set; } = "";
    public List<DetalleItem> Detalle { get; set; } = new();
}

public class DetalleItem
{
    // si querés que Mongo genere un _id por subdocumento, podés agregar:
    // [BsonId] public ObjectId? Id { get; set; }
    public int? Id { get; set; }          // opcional, si los generás en front
    public string? Descripcion { get; set; }
    public string? A { get; set; }
    public string? B { get; set; }
    public string? Observaciones { get; set; }
    public decimal Importe { get; set; }
}
