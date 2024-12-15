using bailable_api.Dtos;
using bailable_api.Service;
using Microsoft.AspNetCore.Mvc;

namespace bailable_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventosController : ControllerBase
{
    private readonly IEventoService _eventoService;
    public EventosController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet]
    public ActionResult GetEventByDate([FromQuery] DateTime date)
    {
        var result = _eventoService.GetEventsByDate(date);
        return Ok(result);
    }
    [HttpPost]
    public ActionResult CreateEvento(CreateEventoRequestDto createEventoRequest)
    {
        CreateEventoResponseDto response = _eventoService.CreateEvento(createEventoRequest);
        if (response.Success)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpPost("cancelar")]
    public ActionResult CancelEvent([FromBody] CancelEventoRequestDto cancelEventoReqDto)
    {
        ActionResult res;

        try
        {
            _eventoService.DeleteEvento(cancelEventoReqDto);
            res = Ok();
        }
        catch (Exception ex)
        {
            res = Problem(ex.Message);
        }

        return res;
    }
    [HttpGet("locales/{localId}")]
    public ActionResult GetEventosByLocal([FromRoute] Guid localId)
    {
        var result = _eventoService.GetEventosByLocalId(localId);
        return Ok(result);
    }
}
