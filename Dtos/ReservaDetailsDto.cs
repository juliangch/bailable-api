namespace bailable_api.Dtos;

public class ReservaDetailsDto
{
    public float Precio { get; set; }
    public string NombreEvento { get; set; }
    public DateTime Fecha { get; set; }
    public int CantidadPersonas { get; set; }
}
