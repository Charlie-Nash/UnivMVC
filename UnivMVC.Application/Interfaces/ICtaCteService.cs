using UnivMVC.Domain.CtaCte;

namespace UnivMVC.Application.Interfaces
{
    public interface ICtaCteService
    {
        Task<PagoMatriculaResult> ConsultarPagoMatriculaAsync(int estudianteId, int semestreId, int categoriaId);
        Task<List<EstadoCtaCte>> ConsultarEstadoCtaCteAsync(int estudianteId, int semestreId);
    }
}
