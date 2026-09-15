using CasoPropuesto_5.Dtos;
using CasoPropuesto_5.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace CasoPropuesto_5.Controllers;

[ApiController]
[Route("api/proveedores")]
public class ProveedoresController : ControllerBase
{
    private readonly IProveedorService _servicio;

    public ProveedoresController(IProveedorService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        return Ok(await _servicio.ObtenerTodosAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var proveedor = await _servicio.ObtenerPorIdAsync(id);
        return proveedor is null
            ? NotFound(new { mensaje = $"No existe el proveedor {id}." })
            : Ok(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] ProveedorDto dto)
    {
        try
        {
            var creado = await _servicio.CrearAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ProveedorDto dto)
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

    [HttpPatch("{id:int}/evaluacion")]
    public async Task<IActionResult> Evaluar(int id, [FromBody] EvaluacionProveedorDto dto)
    {
        try
        {
            return Ok(await _servicio.EvaluarAsync(id, dto.EvaluacionDesempenio));
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
