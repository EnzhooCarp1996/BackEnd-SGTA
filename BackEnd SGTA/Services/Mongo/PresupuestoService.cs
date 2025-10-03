using BackEndSGTA.Data;
using BackEndSGTA.Models;
using MongoDB.Driver;

namespace BackEndSGTA.Services;

public class PresupuestoService
{
    private readonly IMongoCollection<Presupuesto> _presupuestos;

    public PresupuestoService(MongoDbContext context)
    {
        _presupuestos = context.Presupuestos;
    }

    public async Task<List<Presupuesto>> GetAllAsync() =>
        await _presupuestos.Find(_ => true).ToListAsync();

    public async Task<Presupuesto> GetByIdAsync(string id) =>
        await _presupuestos.Find(p => p.IdPresupuesto == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Presupuesto presupuesto) =>
        await _presupuestos.InsertOneAsync(presupuesto);

    public async Task UpdateAsync(string id, Presupuesto presupuesto) =>
        await _presupuestos.ReplaceOneAsync(p => p.IdPresupuesto == id, presupuesto);

    public async Task DeleteAsync(string id) =>
        await _presupuestos.DeleteOneAsync(p => p.IdPresupuesto == id);
}
