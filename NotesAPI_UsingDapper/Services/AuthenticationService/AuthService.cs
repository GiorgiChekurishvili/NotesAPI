using AutoMapper;
using NotesAPI_UsingDapper.DTOs.UserDTO;
using NotesAPI_UsingDapper.Models;
using NotesAPI_UsingDapper.Repositories.AuthenticationRepository;
using System.Security.Cryptography;

namespace NotesAPI_UsingDapper.Services.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<string?> UserLogin(UserLoginDTO userLoginDTO)
        {
            var token = await _authRepository.UserLogin(userLoginDTO.Email, userLoginDTO.Password);
            return token;
        }

        public async Task<UserRegisterDTO?> UserRegister(UserRegisterDTO userRegisterDTO)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userRegisterDTO.Password, out passwordHash, out passwordSalt);
            var map = _mapper.Map<User>(userRegisterDTO);
            map.PasswordHash = passwordHash;
            map.PasswordSalt = passwordSalt;
            var ifexists = await _authRepository.UserRegister(map);
            if (ifexists == null)
            {
                return null;
            }
            return userRegisterDTO;
        }

        private void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
