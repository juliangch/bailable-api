using bailable_api.Dtos;
using bailable_api.Service;
using Microsoft.AspNetCore.Mvc;

namespace bailable_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiciosController : ControllerBase
{
    private readonly IServicioService _servicioService;
    public ServiciosController(IServicioService servicioService)
    {
        _servicioService = servicioService;
    }

    [HttpGet]
    public ActionResult GetServiciosByEvento([FromQuery] Guid eventoId)
    {
        ServiciosByEventoResponseDto response = _servicioService.getServiciosByEvento(eventoId);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
