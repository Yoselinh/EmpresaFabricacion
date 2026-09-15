using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace CasoPropuesto_5.Controllers;

[ApiController]
[Route("api/materias-primas")]
public class MateriasPrimasController : ControllerBase
{
    private readonly IInventarioService _servicio;

    public MateriasPrimasController(IInventarioService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        return Ok(await _servicio.ObtenerTodasAsync());
    }

    [HttpGet("alertas")]
    public async Task<IActionResult> ObtenerAlertas()
    {
        var alertas = (await _servicio.ObtenerAlertasAsync()).ToList();
        return Ok(new
        {
            hayAlertas = alertas.Count > 0,
            total = alertas.Count,
            mensaje = alertas.Count == 0
                ? "No hay materias primas por reabastecer."
                : "Hay materiales que necesitan reabastecimiento.",
            items = alertas
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var materia = await _servicio.ObtenerPorIdAsync(id);
        return materia is null
            ? NotFound(new { mensaje = $"No existe la materia prima {id}." })
            : Ok(materia);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] MateriaPrimaDto dto)
    {
        try
        {
            var creada = await _servicio.CrearAsync(dto);
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] MateriaPrimaDto dto)
    {
        try
        {
            return Ok(await _servicio.ActualizarAsync(id, dto));
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

    [HttpPatch("{id:int}/reabastecer")]
    public async Task<IActionResult> Reabastecer(int id, [FromBody] ReabastecerDto dto)
    {
        try
        {
            return Ok(await _servicio.ReabastecerAsync(id, dto.Cantidad));
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
