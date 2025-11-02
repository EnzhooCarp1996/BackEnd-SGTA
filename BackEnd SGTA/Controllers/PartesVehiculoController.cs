using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndSGTA.Services;
using BackEndSGTA.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PartesVehiculoController : ControllerBase
{
    private readonly PartesVehiculoService _service;

    public PartesVehiculoController(PartesVehiculoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetById(string id)
    {
        var parte = await _service.GetByIdAsync(id);
        if (parte == null) return NotFound();
        return Ok(parte);
    }

    [HttpGet("categoria/{categoria}")]
    public async Task<IActionResult> GetByCategoria(string categoria)
    {
        var parte = await _service.GetByCategoriaAsync(categoria);
        if (parte == null) return NotFound();
        return Ok(parte);
    }

    [HttpPost("agregar-componente")]
    public async Task<IActionResult> AddComponent([FromBody] NuevaParteDto dto)
    {
        try
        {
            var resultado = await _service.AddComponentAsync(dto.Categoria, dto.Componente);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }

    }
}
