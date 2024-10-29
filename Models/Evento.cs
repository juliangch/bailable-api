using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bailable_api.Models;

public class Evento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid EventoId { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public required DateTime Fecha { get; set; }
    public List<Servicio>? Servicios { get; set; } = new List<Servicio>();
    public required string ImgSource { get; set; }
    public required Local Local { get; set; }
}
