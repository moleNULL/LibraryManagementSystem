using AutoMapper;
using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

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
            CreateMap<GenreEntity, GenreDto>();
            CreateMap<BookDataModel, BookDto>();
        }
    }
}
