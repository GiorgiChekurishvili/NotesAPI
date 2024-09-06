using AutoMapper;
using NotesAPI_UsingDapper.DTOs.NotesDTO;
using NotesAPI_UsingDapper.DTOs.UserDTO;
using NotesAPI_UsingDapper.Models;

namespace NotesAPI_UsingDapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notes, NotesReadDTO>();
            CreateMap<NotesCreateDTO, Notes>();
            CreateMap<UserRegisterDTO, User>();
        }
    }
}
