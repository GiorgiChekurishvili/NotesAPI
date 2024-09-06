using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NotesAPI_UsingDapper.DTOs.NotesDTO;
using NotesAPI_UsingDapper.Models;
using NotesAPI_UsingDapper.Repositories.NotesRepository;


namespace NotesAPI_UsingDapper.Services.NotesService
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public NotesService(INotesRepository notesRepository, IMapper mapper, IDistributedCache cache)
        {
            _notesRepository = notesRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task CreateNote(NotesCreateDTO notes, int userId)
        {
            var map = _mapper.Map<Notes>(notes);
            map.UserId = userId;
            await _notesRepository.CreateNote(map);

            await ResetReadAllNotesAfterChange(userId);
        }

        public async Task DeleteNote(int id, int userId)
        {
            await _notesRepository.DeleteNote(id, userId);

            await ResetReadNotesByIdAfterChange(id, userId);
            await ResetReadAllNotesAfterChange(userId);
        }

        public async Task<IEnumerable<NotesReadDTO>> ReadAllNotes(int userId)
        {
            var cachekey = $"ReadAllNotes-{userId}";
            var cachedata = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cachedata))
            {
                return JsonConvert.DeserializeObject<IEnumerable<NotesReadDTO>>(cachedata)!;
            }
            var notes = await _notesRepository.ReadAllNotes(userId);
            var map = _mapper.Map<IEnumerable<NotesReadDTO>>(notes);

            var cacheoptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheoptions);
            return map;
        }

        public async Task<NotesReadDTO?> ReadNoteById(int id, int userId)
        {
            var cachekey = $"ReadNoteById-{id}/{userId}";
            var cachedata = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cachedata))
            {
                return JsonConvert.DeserializeObject<NotesReadDTO>(cachedata)!;
            }

            var note = await _notesRepository.ReadNoteById(id, userId);
            var map = _mapper.Map<NotesReadDTO>(note);

            var cacheoptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheoptions);
            return map;
        }

        public async Task UpdateNote( NotesCreateDTO notes, int id, int userId)
        {
            var map = _mapper.Map<Notes>(notes);
            map.Id = id;    
            map.UserId = userId;
            await _notesRepository.UpdateNote(map);

            await ResetReadAllNotesAfterChange(userId);
            await ResetReadNotesByIdAfterChange(id,userId);
            
        }


        private async Task ResetReadAllNotesAfterChange(int userId)
        {
            var cachekey = $"ReadAllNotes-{userId}";
            await _cache.RemoveAsync(cachekey);
        }
        private async Task ResetReadNotesByIdAfterChange(int id, int userId)
        {
            var cachekey = $"ReadNoteById-{id}/{userId}";
            await _cache.RemoveAsync(cachekey);
        }
    }
}
