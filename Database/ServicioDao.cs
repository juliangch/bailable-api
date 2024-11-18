using bailable_api.Models;

namespace bailable_api.Database
{
    public class ServicioDao
    {
        private readonly ContextDb _dbContext;

        public ServicioDao(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public Servicio GetServicioById(Guid id)
        {
            Servicio servicio = _dbContext.Servicios.FirstOrDefault(s => s.ServicioId == id);
            return servicio;
        }

        public List<Servicio> GetServiciosByEvent(Guid eventId)
        {
            List<Servicio> servicios = _dbContext.Servicios.Where(s => s.Evento.EventoId == eventId).ToList();
            return servicios;
        }
    }
}
