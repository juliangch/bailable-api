using bailable_api.Models;

namespace bailable_api.Dtos;

public class CreateRerservaRequestDto
{
    public Guid UserId { get; set; }
    public Guid EventoId { get; set; }
    public List<Guid> ServiciosIds { get; set; }
    public int CantidadPersonas { get; set; }
    public float Precio { get; set; }
    public User? User { get; set; }
    public Evento? Evento { get; set; }
    public List<Servicio>? Servicios { get; set; } = new List<Servicio>();
}