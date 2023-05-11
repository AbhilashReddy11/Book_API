
using AutoMapper;
using Book_API.Models;
using Book_API.Models.DTO;

namespace Books_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()

        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
           // CreateMap<AuthorDTO, AuthorCreateDTO>().ReverseMap();
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
           // CreateMap<AuthorDTO, AuthorUpdateDTO>().ReverseMap();
            CreateMap<Author, AuthorUpdateDTO>().ReverseMap();

            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<PublisherDTO, PublisherCreateDTO>().ReverseMap();
            CreateMap<PublisherDTO, PublisherUpdateDTO>().ReverseMap();
            CreateMap<Publisher, PublisherCreateDTO>().ReverseMap();
            CreateMap<Publisher, PublisherUpdateDTO>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookDTO, BookCreateDTO>().ReverseMap();
            CreateMap<BookDTO, BookUpdateDTO>().ReverseMap();
            CreateMap<LocalUser, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserUpdateDTO>().ReverseMap();
           // CreateMap<AuthorDTO, AuthorUpdateDTO>().ReverseMap();

        }
    }
}

