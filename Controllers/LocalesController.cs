using bailable_api.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bailable_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalesController : ControllerBase
{

    [HttpPost]
    public ActionResult CreateLocal(RegisterLocalRequestDto registerLocalRequestDto)
    {

        return Ok();
    }
}
