using System.Net;
using System.Net.Http.Json;
using UnivMVC.Application.Interfaces;
using UnivMVC.Domain.Auth;

namespace UnivMVC.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login", request);

                var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

                if (loginResult == null)
                {
                    return new LoginResult
                    {
                        status = HttpStatusCode.InternalServerError,
                        mensaje = "La respuesta del servidor no tiene el formato esperado"
                    };
                }

                return loginResult;
            }
            catch
            {
                return new LoginResult
                {
                    status = HttpStatusCode.InternalServerError,
                    mensaje = "Error de comunicación con el servicio"
                };
            }
        }

        public async Task<User> Login2faAsync(TfaRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login2fa", request);

                var user = await response.Content.ReadFromJsonAsync<User>();

                if (user == null)
                {
                    return new User
                    {
                        status = HttpStatusCode.InternalServerError,
                        mensaje = "La respuesta del servidor no tiene el formato esperado"
                    };
                }

                return user;
            }
            catch
            {
                return new User
                {
                    status = HttpStatusCode.InternalServerError,
                    mensaje = "Error de comunicación con el servicio"
                };
            }
        }
    }
}
