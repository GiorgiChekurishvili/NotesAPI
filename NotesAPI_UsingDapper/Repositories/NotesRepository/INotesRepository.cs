using NotesAPI_UsingDapper.Models;

namespace NotesAPI_UsingDapper.Repositories.NotesRepository
{
    public interface INotesRepository
    {
        Task<IEnumerable<Notes>> ReadAllNotes(int userId);
        Task<Notes?> ReadNoteById(int id, int userId);
        Task CreateNote(Notes notes);
        Task UpdateNote(Notes notes);
        Task DeleteNote(int id, int userId);
    }
}
