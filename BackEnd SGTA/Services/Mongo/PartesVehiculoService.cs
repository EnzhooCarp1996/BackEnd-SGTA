using BackEndSGTA.Data.Mongo;
using BackEndSGTA.Models;
using MongoDB.Driver;

namespace BackEndSGTA.Services;

public class PartesVehiculoService
{
    private readonly IMongoCollection<PartesVehiculo> _partes;

    public PartesVehiculoService(MongoDbContext context)
    {
        _partes = context.PartesVehiculo;
    }

    public async Task<List<PartesVehiculo>> GetAllAsync() =>
        await _partes.Find(_ => true).ToListAsync();

    public async Task<PartesVehiculo?> GetByIdAsync(string id) =>
        await _partes.Find(p => p.Id == id).FirstOrDefaultAsync();

    // Ejemplo: traer solo por categoría
    public async Task<PartesVehiculo?> GetByCategoriaAsync(string categoria) =>
        await _partes.Find(p => p.Categoria == categoria).FirstOrDefaultAsync();
}



