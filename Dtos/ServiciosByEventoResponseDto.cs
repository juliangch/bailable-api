using bailable_api.Models;

namespace bailable_api.Dtos;

public class ServiciosByEventoResponseDto
{
    public List<Servicio> Servicios { get; set; }
    public bool Success { get; set; }
}
