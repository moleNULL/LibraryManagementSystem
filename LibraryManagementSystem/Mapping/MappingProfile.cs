using AutoMapper;
using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.ViewModels;

namespace LibraryManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapBooks();
        }

        private void MapBooks()
        {
            CreateMap<BookDataModel, BookDto>().ReverseMap();
            CreateMap<BookDto, BookViewModel>().ReverseMap();
        }
    }
}
