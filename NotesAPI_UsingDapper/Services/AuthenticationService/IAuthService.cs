using NotesAPI_UsingDapper.DTOs.UserDTO;

namespace NotesAPI_UsingDapper.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task<string?> UserLogin(UserLoginDTO userLoginDTO);
        Task<UserRegisterDTO?> UserRegister(UserRegisterDTO userRegisterDTO);
    }
}
