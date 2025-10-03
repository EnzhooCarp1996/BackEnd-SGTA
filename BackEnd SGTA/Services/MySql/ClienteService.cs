using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Models;
using BackEndSGTA.Data;

namespace BackEndSGTA.Services;

public class ClienteService
{
    private readonly AppDbContext _context;

    public ClienteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetClienteAllAsync() =>
        await _context.Clientes
            .Include(c => c.Vehiculos)
            .AsSplitQuery()
            .ToListAsync();

    public async Task<Cliente?> GetClienteByIdAsync(int id) =>
        await _context.Clientes
            .Include(c => c.Vehiculos)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.IdCliente == id);

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task UpdateClienteAsync(int id, Cliente cliente)
    {
        var existing = await _context.Clientes.FindAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Cliente {id} no encontrado");

        _context.Entry(existing).CurrentValues.SetValues(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClienteAsync(int id)
    {
        var existing = await _context.Clientes.FindAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"Cliente {id} no encontrado");

        _context.Clientes.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
