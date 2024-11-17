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
            return Ok("Usuario creado con exito.");

        }
        else
        {
            return BadRequest("Error al registrar usuario.");
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
}
