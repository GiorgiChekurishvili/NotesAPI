using System.ComponentModel.DataAnnotations;

namespace NotesAPI_UsingDapper.DTOs.UserDTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [EmailAddress, Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
