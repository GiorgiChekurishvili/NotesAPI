using AutoMapper;
using NotesAPI_UsingDapper.DTOs.NotesDTO;
using NotesAPI_UsingDapper.Models;
using NotesAPI_UsingDapper.Repositories.NotesRepository;


namespace NotesAPI_UsingDapper.Services.NotesService
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IMapper _mapper;

        public NotesService(INotesRepository notesRepository, IMapper mapper)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
        }

        public async Task CreateNote(NotesCreateDTO notes, int userId)
        {
            var map = _mapper.Map<Notes>(notes);
            map.UserId = userId;
            await _notesRepository.CreateNote(map);
        }

        public async Task DeleteNote(int id, int userId)
        {
            await _notesRepository.DeleteNote(id, userId);
        }

        public async Task<IEnumerable<NotesReadDTO>> ReadAllNotes(int userId)
        {
            var notes = await _notesRepository.ReadAllNotes(userId);
            var map = _mapper.Map<IEnumerable<NotesReadDTO>>(notes);
            return map;
        }

        public async Task<NotesReadDTO?> ReadNoteById(int id, int userId)
        {
            var note = await _notesRepository.ReadNoteById(id, userId);
            var map = _mapper.Map<NotesReadDTO>(note);
            return map;
        }

        public async Task UpdateNote( NotesCreateDTO notes, int id, int userId)
        {
            var map = _mapper.Map<Notes>(notes);
            map.Id = id;    
            map.UserId = userId;
            await _notesRepository.UpdateNote(map);
        }
    }
}
