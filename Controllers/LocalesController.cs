using bailable_api.Dtos;
using bailable_api.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bailable_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalesController : ControllerBase
{
    private readonly ILocalService _localService;

    public LocalesController(ILocalService localService)
    {
        _localService = localService;
    }
    [HttpPost]
    public ActionResult CreateLocal(RegisterLocalRequestDto registerLocalRequestDto)
    {
        bool result = _localService.CreateLocal(registerLocalRequestDto);
        if (result)
        {
            return Ok("Local creado con exito.");

        }
        else
        {
            return BadRequest("Error al crear local.");
        }
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteLocal([FromRoute] Guid id)
    {
        DeleteLocalResponseDto result = _localService.DeleteLocal(id);
        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut("editar")]
    public ActionResult EditLocal([FromBody] EditLocalRequestDto editLocalReqDto)
    {
        try
        {
            _localService.CheckLocalOwnership(editLocalReqDto.Local.LocalId, editLocalReqDto.UserId);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }

        try
        {
            _localService.EditLocalByOwner(editLocalReqDto);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }

        return Ok();

    }
    [HttpGet]
    public ActionResult GetLocalesByDuenio([FromQuery] Guid duenioId)
    {
        return Ok(_localService.GetLocalesByDuenioId(duenioId));
    }
    [HttpGet("{id}")]
    public ActionResult GetLocalById(Guid id)
    {
        return Ok(_localService.GetLocalById(id));
    }
}
