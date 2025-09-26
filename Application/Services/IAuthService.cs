using Contract.Requests;
using Contract.Responses;

namespace Application.Services
{
    public interface IAuthService
    {
        AuthResponse? Register(RegisterRequest request);
        AuthResponse? Login(LoginRequest request);
    }
}
