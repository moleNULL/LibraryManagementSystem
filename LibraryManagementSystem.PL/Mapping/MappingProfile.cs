using AutoMapper;
using LibraryManagementSystem.BLL.Models.DataModels;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.PL.Models.ViewModels;

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
            CreateMap<BookDataModel, BookDto>().ForMember("PictureName", options =>
            {
                options.MapFrom<BookPictureResolver, string>(b => b.PictureName);
            });

            CreateMap<BookDataModel, BookDto>().ReverseMap();
            CreateMap<BookDto, BookViewModel>().ReverseMap();

            CreateMap<BookAddViewModel, BookDto>();
            CreateMap<BookUpdateViewModel, BookDto>();
        }
    }
}
