using UnivMVC.Domain.Auth;

namespace UnivMVC.Application.Interfaces
{
    public interface ILoginService
    {        
        Task<LoginResult> LoginAsync(LoginRequest request);
        Task<User> Login2faAsync(TfaRequest request);
    }
}
