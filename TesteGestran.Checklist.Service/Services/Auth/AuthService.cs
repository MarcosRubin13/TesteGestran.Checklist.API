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

        public Domain.Entities.User IsValidUser(string username, string password)
        {
            return _authRepository.GetUserByUsernameAndPassword(username, password);
        }
    }
}
