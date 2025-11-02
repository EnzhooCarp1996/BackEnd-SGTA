using BackEndSGTA.Data.Mongo;
using BackEndSGTA.Validators;
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

    public async Task CreateAsync(PartesVehiculo nuevaParte) =>
        await _partes.InsertOneAsync(nuevaParte);

    public async Task UpdateAsync(string id, PartesVehiculo parteActualizada) =>
        await _partes.ReplaceOneAsync(p => p.Id == id, parteActualizada);

    public async Task<string> AddComponentAsync(string categoria, string componente)
    {
        // Validación básica
        PartesVehiculoValidator.ValidarNombre(categoria);
        PartesVehiculoValidator.ValidarNombre(componente);

        var categoriaExistente = await GetByCategoriaAsync(categoria);

        if (categoriaExistente == null)
        {
            var nuevaCategoria = new PartesVehiculo
            {
                Categoria = categoria,
                Componentes = new List<Componente>
                {
                    new Componente { Nombre = componente }
                }
            };

            await CreateAsync(nuevaCategoria);
            return "Categoría creada y componente agregado";
        }

        if (PartesVehiculoValidator.ComponenteExiste(categoriaExistente, componente))
            return "El componente ya existe en esta categoría";

        categoriaExistente.Componentes.Add(new Componente { Nombre = componente });
        await UpdateAsync(categoriaExistente.Id, categoriaExistente);

        return "Componente agregado a la categoría existente";
    }
}



