using System.ComponentModel.DataAnnotations;

namespace NotesAPI_UsingDapper.DTOs.NotesDTO
{
    public class NotesReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
