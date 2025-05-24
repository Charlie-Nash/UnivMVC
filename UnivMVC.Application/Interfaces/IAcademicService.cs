using UnivMVC.Application.DTOs;
using UnivMVC.Domain.Academico;

namespace UnivMVC.Application.Interfaces
{
    public interface IAcademicService
    {
        Task<Estudiante> ObtenerEstudianteInfoAsync(int personaId);
        Task<List<OfertaAcad>> ObtenerOfertaAcademicaAsync(int estudianteId);
        Task<List<CursoMatriculado>> RegistrarMatriculaAsync(MatriculaDto matricula);
        Task<List<CursoMatriculado>> ObtenerCursosMatriculadosAsync(int matriculaId);
    }
}
