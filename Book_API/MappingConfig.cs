
using AutoMapper;
using Book_API.Models;
using Book_API.Models.DTO;

namespace Books_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()

        {
           
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
           

           
            CreateMap<Publisher, PublisherCreateDTO>().ReverseMap();
            

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookDTO, BookCreateDTO>().ReverseMap();
           
            CreateMap<Book, BookCreateDTO>().ReverseMap();
           

          
          

        }
    }
}

