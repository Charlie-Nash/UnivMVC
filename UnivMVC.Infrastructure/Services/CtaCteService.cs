using System.Net;
using System.Net.Http.Json;
using UnivMVC.Application.DTOs;
using UnivMVC.Application.Interfaces;
using UnivMVC.Domain.Academico;
using UnivMVC.Domain.CtaCte;

namespace UnivMVC.Infrastructure.Services
{
    public class CtaCteService : ICtaCteService
    {
        private readonly HttpClient _httpClient;

        public CtaCteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagoMatriculaResult> ConsultarPagoMatriculaAsync(int estudianteId, int semestreId, int categoriaId)
        {
            try
            {
                PagoMatriculaDto request = new();

                request.estudiante_id = estudianteId;
                request.semestre_id = semestreId;
                request.categoria_id = categoriaId;

                var response = await _httpClient.GetAsync($"estudiante/ctacte/pago-matricula/{estudianteId}/{semestreId}/{categoriaId}");

                var result = await response.Content.ReadFromJsonAsync<PagoMatriculaResult>();

                if (result == null)
                {
                    return new PagoMatriculaResult()
                    {
                        status = HttpStatusCode.InternalServerError,
                        mensaje = "Error de conexión con el servicio"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new PagoMatriculaResult()
                {
                    status = HttpStatusCode.InternalServerError,
                    mensaje = ex.Message
                };
            }
        }

        public async Task<List<EstadoCtaCte>> ConsultarEstadoCtaCteAsync(int estudianteId, int semestreId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"estudiante/ctacte/estado/{estudianteId}/{semestreId}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<EstadoCtaCte>();
                }

                var lista = await response.Content.ReadFromJsonAsync<List<EstadoCtaCte>>();

                return lista ?? new List<EstadoCtaCte>();
            }
            catch
            {
                return new List<EstadoCtaCte>();
            }
        }
    }
}
