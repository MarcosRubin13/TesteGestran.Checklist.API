namespace TesteGestran.Checklist.Domain.Interfaces.Service
{
    public interface IAuthService
    {
        Entities.User IsValidUser(string username, string password);
    }
}
