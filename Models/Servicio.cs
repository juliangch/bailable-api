using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bailable_api.Models;

public class Servicio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ServicioId { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public required float Precio { get; set; }
    public required Evento Evento { get; set; }
    public List<Reserva>? Reservas { get; set; } = new List<Reserva>();
}
