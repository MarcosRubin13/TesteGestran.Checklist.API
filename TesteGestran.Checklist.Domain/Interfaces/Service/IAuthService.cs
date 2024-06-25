namespace TesteGestran.Checklist.Domain.Interfaces.Service
{
    public interface IAuthService
    {
        bool IsValidUser(string username, string password);
    }
}
