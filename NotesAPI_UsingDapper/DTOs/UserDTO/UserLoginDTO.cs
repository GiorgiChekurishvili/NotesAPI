using System.ComponentModel.DataAnnotations;

namespace NotesAPI_UsingDapper.DTOs.UserDTO
{
    public class UserLoginDTO
    {
        [EmailAddress, Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
