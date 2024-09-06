using Dapper;
using Microsoft.Data.SqlClient;
using NotesAPI_UsingDapper.Models;

namespace NotesAPI_UsingDapper.Repositories.NotesRepository
{
    public class NotesRepository : INotesRepository
    {
        private readonly IConfiguration _configuration;

        public NotesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task CreateNote(Notes notes)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                await connection.ExecuteAsync("INSERT INTO Notes (Title, Content, UserId, DateCreated) VALUES (@Title, @Content, @UserId, @DateCreated)", notes);
                connection.Close();
            }
        }

        public async Task DeleteNote(int id, int userId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                await connection.ExecuteAsync("DELETE FROM Notes WHERE UserId = @UserId AND Id = @Id", new { Id = id, UserId = userId });
                connection.Close();
            }
        }

        public async Task<IEnumerable<Notes>> ReadAllNotes(int userId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var notes = await connection.QueryAsync<Notes>("SELECT * FROM Notes WHERE UserId = @UserId", new {UserId = userId });
                return notes;
            }
        }

        public async Task<Notes?> ReadNoteById(int id, int userId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var note = await connection.QueryFirstOrDefaultAsync<Notes>("SELECT * FROM Notes WHERE ID = @Id AND UserId = @UserId", new { Id = id, UserId = userId });
                if (note == null)
                {
                    return null;
                }
                return note;
            }
        }


        public async Task UpdateNote(Notes notes)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                await connection.ExecuteAsync("UPDATE Notes SET Title = @Title, Content = @Content, UserId = @UserId, DateModified = @DateModified WHERE Id = @Id", notes);
                connection.Close();
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
