using TesteGestran.Checklist.Domain.Interfaces.Infra;
using TesteGestran.Checklist.Domain.Interfaces.Service;

namespace TesteGestran.Checklist.Service.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public bool IsValidUser(string username, string password)
        {
            var user = _authRepository.GetUserByUsernameAndPassword(username, password);
            return user != null;
        }
    }
}
