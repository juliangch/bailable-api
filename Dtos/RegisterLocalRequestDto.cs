using System.ComponentModel.DataAnnotations;

namespace bailable_api.Dtos;

public class RegisterLocalRequestDto
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public int Capacidad { get; set; }
    [Required]
    public string Direccion { get; set; }
    [Required]
    public string Zona { get; set; }
}
