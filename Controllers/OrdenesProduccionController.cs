using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace CasoPropuesto_5.Controllers;

[ApiController]
[Route("api/ordenes-produccion")]
public class OrdenesProduccionController : ControllerBase
{
    private readonly IProduccionService _servicio;

    public OrdenesProduccionController(IProduccionService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas([FromQuery] string? estado)
    {
        if (!string.IsNullOrWhiteSpace(estado))
        {
            return Ok(await _servicio.ObtenerPorEstadoAsync(estado));
        }

        return Ok(await _servicio.ObtenerTodasAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var orden = await _servicio.ObtenerPorIdAsync(id);
        return orden is null ? NotFound(new { mensaje = $"No existe la orden {id}." }) : Ok(orden);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] OrdenProduccionDto dto)
    {
        try
        {
            var creada = await _servicio.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] OrdenProduccionDto dto)
    {
        try
        {
            return Ok(await _servicio.ActualizarAsync(id, dto));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoDto dto)
    {
        try
        {
            return Ok(await _servicio.CambiarEstadoAsync(id, dto.Estado));
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
