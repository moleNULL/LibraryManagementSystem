using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.PL.ViewModels.BookViewModels;

namespace LibraryManagementSystem.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapBooks();
        }

        private void MapBooks()
        {
            CreateMap<BookEntity, BookDto>().ForMember("PictureName", options =>
            {
                options.MapFrom<BookPictureResolver, string?>(b => b.PictureName);
            });
            
            CreateMap<BookDto, BookViewModel>().ReverseMap();

            CreateMap<BookEntity, BookDto>().ForMember("GenreIds", options =>
            {
                options.MapFrom<BookIdsResolver, ICollection<BookGenreEntity>>(bookEntity => bookEntity.BookGenres);
            });

            CreateMap<BookDto, BookEntity>().ForMember("BookGenres", options =>
            {
                options.MapFrom<BookGenresResolver, IEnumerable<int>>(bookDto => bookDto.GenreIds);
            });

            CreateMap<BookAddViewModel, BookDto>();
            CreateMap<BookUpdateViewModel, BookDto>();
        }
    }
}
