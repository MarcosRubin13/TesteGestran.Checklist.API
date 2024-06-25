namespace TesteGestran.Checklist.Domain.Interfaces.Infra
{
    public interface IAuthRepository
    {
        Entities.User GetUserByUsernameAndPassword(string username, string password);
    }
}
