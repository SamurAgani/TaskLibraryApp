using AutoMapper;
using TaskLibraryApp.Entities;
using TaskLibraryApp.Models;

namespace TaskLibraryApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDetailsVM>().ReverseMap();
            CreateMap<Book, CreateUpdateBookVM>().ReverseMap();
        }
    }
}
