using CasoPropuesto_5.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace CasoPropuesto_5.Controllers;

[ApiController]
[Route("api/informes")]
public class InformesController : ControllerBase
{
    private readonly IInformeService _servicio;

    public InformesController(IInformeService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerGeneral()
    {
        return Ok(await _servicio.ObtenerInformeGeneralAsync());
    }

    [HttpGet("produccion")]
    public async Task<IActionResult> ObtenerProduccion()
    {
        return Ok(await _servicio.ObtenerInformeProduccionAsync());
    }

    [HttpGet("calidad")]
    public async Task<IActionResult> ObtenerCalidad()
    {
        return Ok(await _servicio.ObtenerInformeCalidadAsync());
    }

    [HttpGet("inventario")]
    public async Task<IActionResult> ObtenerInventario()
    {
        return Ok(await _servicio.ObtenerInformeInventarioAsync());
    }

    [HttpGet("proveedores")]
    public async Task<IActionResult> ObtenerProveedores()
    {
        return Ok(await _servicio.ObtenerInformeProveedoresAsync());
    }
}
