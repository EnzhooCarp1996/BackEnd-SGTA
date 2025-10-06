using BackEndSGTA.Data.Mongo;
using BackEndSGTA.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackEndSGTA.Services;

public class PresupuestoService
{
    private readonly IMongoCollection<Presupuesto> _presupuestos;

    public PresupuestoService(MongoDbContext context)
    {
        _presupuestos = context.Presupuestos;
    }

    public async Task<List<Presupuesto>> GetAllPresupuestosAsync() =>
        await _presupuestos.Find(_ => true).ToListAsync();

    public async Task<Presupuesto> GetByIdPresupuestoAsync(string id)
    {
        return await _presupuestos.Find(p => p._id == id).FirstOrDefaultAsync();
    }

    public async Task CreatePresupuestoAsync(Presupuesto presupuesto) =>
        await _presupuestos.InsertOneAsync(presupuesto);

    public async Task UpdatePresupuestoAsync(string id, Presupuesto presupuesto)
    {
        await _presupuestos.ReplaceOneAsync(p => p._id == id, presupuesto);
    }

    public async Task DeletePresupuestoAsync(string id)
    {
        await _presupuestos.DeleteOneAsync(p => p._id == id);
    }
}
