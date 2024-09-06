using NotesAPI_UsingDapper.Models;

namespace NotesAPI_UsingDapper.Repositories.AuthenticationRepository
{
    public interface IAuthRepository
    {
        Task<User?> UserRegister(User user);
        Task<string?> UserLogin(string email, string password);
    }
}
