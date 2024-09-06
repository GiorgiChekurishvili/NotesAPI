using System.ComponentModel.DataAnnotations;

namespace NotesAPI_UsingDapper.DTOs.NotesDTO
{
    public class NotesCreateDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
    }
}
