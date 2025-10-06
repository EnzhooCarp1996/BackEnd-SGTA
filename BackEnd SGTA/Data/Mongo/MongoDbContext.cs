using BackEndSGTA.Models;
using MongoDB.Driver;

namespace BackEndSGTA.Data.Mongo;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
    }

    public IMongoCollection<PartesVehiculo> PartesVehiculo =>
        _database.GetCollection<PartesVehiculo>("partesVehiculo");

    public IMongoCollection<Presupuesto> Presupuestos =>
            _database.GetCollection<Presupuesto>("presupuestos");
}
