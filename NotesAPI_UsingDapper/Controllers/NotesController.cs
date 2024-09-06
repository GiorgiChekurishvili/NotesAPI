using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAPI_UsingDapper.DTOs.NotesDTO;
using NotesAPI_UsingDapper.Services.NotesService;
using System.Security.Claims;

namespace NotesAPI_UsingDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }
        [Authorize]
        [HttpGet("readallnotes")]
        public async Task<ActionResult<NotesReadDTO>> ReadAllNotes()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var data = await _notesService.ReadAllNotes(userId);
            return Ok(data);
        }
        [Authorize]
        [HttpGet("readnotesbyid/{id}")]
        public async Task<ActionResult<NotesReadDTO>> ReadNoteById(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var data = await _notesService.ReadNoteById(id, userId);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [Authorize]
        [HttpPost("createnote")]
        public async Task<ActionResult<NotesCreateDTO>> CreateNote(NotesCreateDTO notesCreate)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _notesService.CreateNote(notesCreate, userId);
            return Ok(notesCreate);

        }
        [Authorize]
        [HttpPut("updatenote/{id}")]
        public async Task<ActionResult<NotesCreateDTO>> UpdateNote(int id ,NotesCreateDTO notesCreate)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var data = await _notesService.ReadNoteById(id, userId);
            if (data == null)
            {
                return NotFound();
            }
            await _notesService.UpdateNote(notesCreate, id, userId);
            return Ok();
        }
        [Authorize]
        [HttpDelete("deletenote/{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var data = await _notesService.ReadNoteById(id, userId);
            if (data == null)
            {
                return NotFound();
            }
            await _notesService.DeleteNote(id, userId);
            return Ok();
        }

    }
}
