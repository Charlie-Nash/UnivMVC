using System.Net;
using System.Net.Http.Json;
using UnivMVC.Application.DTOs;
using UnivMVC.Application.Interfaces;
using UnivMVC.Domain.Academico;

namespace UnivMVC.Infrastructure.Services
{
    public class AcademicService : IAcademicService
    {
        private readonly HttpClient _httpClient;

        public AcademicService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Estudiante> ObtenerEstudianteInfoAsync(int personaId)
        {
            try
            {
                PersonaDto request = new();

                request.id = personaId;

                var response = await _httpClient.PostAsJsonAsync("estudiante/matricula/info", request);

                var result = await response.Content.ReadFromJsonAsync<Estudiante>();

                if (result == null)
                {
                    return new Estudiante()
                    {
                        id = 0
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Estudiante()
                {
                    status = HttpStatusCode.InternalServerError,
                    mensaje = ex.Message
                };
            }
        }

        public async Task<List<OfertaAcad>> ObtenerOfertaAcademicaAsync(int estudianteId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"estudiante/matricula/oferta-academica/{estudianteId}");

                if (!response.IsSuccessStatusCode)
                {                    
                    return new List<OfertaAcad>();
                }

                var lista = await response.Content.ReadFromJsonAsync<List<OfertaAcad>>();

                return lista ?? new List<OfertaAcad>();
            }
            catch
            {                
                return new List<OfertaAcad>();
            }
        }

        public async Task<List<CursoMatriculado>> RegistrarMatriculaAsync(MatriculaDto matricula)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("estudiante/matricula/registrar", matricula);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<CursoMatriculado>();
                }

                var lista = await response.Content.ReadFromJsonAsync<List<CursoMatriculado>>();

                return lista ?? new List<CursoMatriculado>();
            }
            catch
            {
                return new List<CursoMatriculado>();
            }
        }

        public async Task<List<CursoMatriculado>> ObtenerCursosMatriculadosAsync(int matriculaId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"estudiante/matricula/cursos-matriculados/{matriculaId}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<CursoMatriculado>();
                }

                var lista = await response.Content.ReadFromJsonAsync<List<CursoMatriculado>>();

                return lista ?? new List<CursoMatriculado>();
            }
            catch
            {
                return new List<CursoMatriculado>();
            }
        }
    }
}
