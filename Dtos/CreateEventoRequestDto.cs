using bailable_api.Models;

namespace bailable_api.Dtos;

public class CreateEventoRequestDto
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }
    public List<ServicioDto>? Servicios { get; set; } = new List<ServicioDto>();
    public string ImgSource { get; set; }
    public Guid LocalId { get; set; }
    public Local? Local { get; set; }
}

