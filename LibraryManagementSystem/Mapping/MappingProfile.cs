using AutoMapper;
using LibraryManagementSystem.BLL.Models;
using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Dtos;

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
            CreateMap<BookDataModel, BookDto>();
            CreateMap<BookDto, BookModel>();
        }
    }
}
