using bailable_api.Dtos;
using bailable_api.Service;
using Microsoft.AspNetCore.Mvc;

namespace bailable_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;
    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpPost]
    public ActionResult CreateReserva(CreateRerservaRequestDto createRerservaRequestDto)
    {
        CreateReservaResponseDto response = _reservaService.CreateReserva(createRerservaRequestDto);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    [HttpGet]
    public ActionResult GetReservasByUserId([FromQuery] Guid userId)
    {
        ReservasByUserResponseDto response = _reservaService.GetReservasByUserId(userId);
        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound("No tenes reservas.");
        }
    }
}
