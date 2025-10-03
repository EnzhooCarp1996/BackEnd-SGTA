using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEndSGTA.Models;
public class PartesVehiculo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("categoria")]
    public string Categoria { get; set; } = null!;

    [BsonElement("componentes")]
    public List<Componente> Componentes { get; set; } = new();
}

public class Componente
{
    [BsonElement("nombre")]
    public string Nombre { get; set; } = null!;

    [BsonElement("subcomponentes")]
    public List<Subcomponente>? Subcomponentes { get; set; }
}

public class Subcomponente
{
    [BsonElement("nombre")]
    public string Nombre { get; set; } = null!;

    [BsonElement("detalles")]
    public List<string>? Detalles { get; set; }
}
