﻿namespace NotesAPI_UsingDapper.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;


        public ICollection<Notes>? Note { get; set; }
    }
}
