using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NotesAPI_UsingDapper.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NotesAPI_UsingDapper.Repositories.AuthenticationRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> UserLogin(string email, string password)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var data = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
                if (data != null)
                {
                    if (VerifyPasswordHash(password,data.PasswordHash!,data.PasswordSalt!))
                    {
                        string token = CreateToken(data);
                        return token;
                    }
                    return null;
                }
                return null;

            }
        }

        public async Task<User?> UserRegister(User user)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var ifexists = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = user.Email });
                if (ifexists == null)
                {
                    await connection.ExecuteAsync("INSERT INTO Users (Name,Surname,Email,PasswordHash,PasswordSalt,DateRegistered)" +
                        "VALUES(@Name,@Surname,@Email,@PasswordHash,@PasswordSalt,@DateRegistered)", user);
                    return user;
                }
                return null;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computehash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computehash.SequenceEqual(passwordHash);
            }
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        private string CreateToken(User user)
        {
            List<Claim>claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred

                );
            var jwt  = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
