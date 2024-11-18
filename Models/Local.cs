using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bailable_api.Models;


public class Local
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid LocalId { get; set; }
    public required string Nombre { get; set; }
    public required int Capacidad { get; set; }
    public required string Direccion { get; set; }
    public required string Zona { get; set; }
    public List<Evento>? Eventos { get; set; } = new List<Evento>();
    public required User Duenio { get; set; }
}
