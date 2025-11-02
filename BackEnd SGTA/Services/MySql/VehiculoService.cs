using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Data.MySql;
using BackEndSGTA.Models;

namespace BackEndSGTA.Services;

public class VehiculoService
{
    private readonly AppDbContext _context;

    public VehiculoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vehiculo>> GetAllVehiculosAsync()
    {
        return await _context.Vehiculos.ToListAsync();
    }

    public async Task<Vehiculo?> GetByIdVehiculoAsync(int id)
    {
        return await _context.Vehiculos.FirstOrDefaultAsync(v => v.IdVehiculo == id);
    }

    public async Task<Vehiculo> CreateVehiculoAsync(Vehiculo vehiculo)
    {
        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();
        return vehiculo;
    }

    public async Task<bool> UpdateVehiculoAsync(int id, Vehiculo vehiculo)
    {
        var existing = await _context.Vehiculos.FindAsync(id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(vehiculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteVehiculoAsync(int id)
    {
        var existing = await _context.Vehiculos.FindAsync(id);
        if (existing == null) return false;

        _context.Vehiculos.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
    // Obtener todas las marcas distintas
    public async Task<List<string>> GetDistinctMarcasAsync()
    {
        return await _context.Vehiculos
            .Select(v => v.Marca)
            .Distinct()
            .OrderBy(m => m)
            .ToListAsync();
    }

    // Obtener todos los modelos según la marca
    public async Task<List<string>> GetModelosByMarcaAsync(string marca)
    {
        return await _context.Vehiculos
            .Where(v => v.Marca == marca)
            .Select(v => v.Modelo)
            .Distinct()
            .OrderBy(m => m)
            .ToListAsync();
    }
}
