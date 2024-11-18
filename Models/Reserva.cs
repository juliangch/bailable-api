using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bailable_api.Models;

public class Reserva
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ReservaId { get; set; }
    public User Usuario { get; set; }
    public Evento Evento { get; set; }
    public List<Servicio> Servicios { get; set; } = new List<Servicio>();
    public int CantidadPersonas { get; set; }
    public float Precio { get; set; }
    public Guid UsuarioId { get; set; }
}
