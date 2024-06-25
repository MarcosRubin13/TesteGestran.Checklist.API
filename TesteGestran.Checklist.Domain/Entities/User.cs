using System.Text;
using TesteGestran.Checklist.Domain.User;

namespace TesteGestran.Checklist.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public static class Factory
    {
        public static User Create(UserCommand command)
        {
            return new User
            {
                Username = command.Username,
                Password = GerarNovaSenha(6),
                Role = command.Role,
            };
        }

        private static string GerarNovaSenha(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder password = new StringBuilder();
            Random random = new();

            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            return password.ToString();
        }

    }
}
