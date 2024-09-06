using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAPI_UsingDapper.DTOs.UserDTO;
using NotesAPI_UsingDapper.Services.AuthenticationService;

namespace NotesAPI_UsingDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDTO>> Register(UserRegisterDTO userRegisterDTO)
        {
            var ifexists = await _authService.UserRegister(userRegisterDTO);
            if (ifexists == null)
            {
                return BadRequest("Email already is registered");
            }
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDTO>> Login(UserLoginDTO userLoginDTO)
        {
            var token = await _authService.UserLogin(userLoginDTO);
            if (token == null)
            {
                BadRequest("Invalid password or email");
            }
            return Ok(token);
        }
    }
}
