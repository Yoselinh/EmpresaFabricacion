using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace CasoPropuesto_5.Controllers;

[ApiController]
[Route("api/inspecciones-calidad")]
public class InspeccionesCalidadController : ControllerBase
{
    private readonly ICalidadService _servicio;

    public InspeccionesCalidadController(ICalidadService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas([FromQuery] string? etapa)
    {
        if (!string.IsNullOrWhiteSpace(etapa))
        {
            return Ok(await _servicio.ObtenerPorEtapaAsync(etapa));
        }

        return Ok(await _servicio.ObtenerTodasAsync());
    }

    [HttpGet("defectuosas")]
    public async Task<IActionResult> ObtenerDefectuosas()
    {
        return Ok(await _servicio.ObtenerDefectuosasAsync());
    }

    [HttpGet("orden/{ordenProduccionId:int}")]
    public async Task<IActionResult> ObtenerPorOrden(int ordenProduccionId)
    {
        return Ok(await _servicio.ObtenerPorOrdenAsync(ordenProduccionId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var inspeccion = await _servicio.ObtenerPorIdAsync(id);
        return inspeccion is null
            ? NotFound(new { mensaje = $"No existe la inspección {id}." })
            : Ok(inspeccion);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] InspeccionCalidadDto dto)
    {
        try
        {
            var creada = await _servicio.RegistrarAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPatch("{id:int}/defectuoso")]
    public async Task<IActionResult> MarcarDefectuoso(int id, [FromBody] MarcarDefectuosoDto? dto)
    {
        try
        {
            return Ok(await _servicio.MarcarDefectuosoAsync(id, dto?.Observaciones));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            await _servicio.EliminarAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }
}
