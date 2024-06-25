using TesteGestran.Checklist.Domain.Entities;
using TesteGestran.Checklist.Domain.Interfaces.Infra;

namespace TesteGestran.Checklist.Infra.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserByUsernameAndPassword(string username, string password) => _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
    }
}
