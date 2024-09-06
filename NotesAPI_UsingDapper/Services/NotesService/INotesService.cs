using NotesAPI_UsingDapper.DTOs.NotesDTO;

namespace NotesAPI_UsingDapper.Services.NotesService
{
    public interface INotesService
    {
        Task<IEnumerable<NotesReadDTO>> ReadAllNotes(int userId);
        Task<NotesReadDTO?> ReadNoteById(int id, int userId);
        Task CreateNote(NotesCreateDTO notes, int userId);
        Task UpdateNote(NotesCreateDTO notes, int id, int userId);
        Task DeleteNote(int id, int userId);
    }
}
