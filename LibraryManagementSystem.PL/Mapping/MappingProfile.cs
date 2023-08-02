using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.PL.ViewModels;
using LibraryManagementSystem.PL.ViewModels.AuthorViewModels;
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
            CreateMap<BookDto, BookViewModel>().ReverseMap();

            CreateMap<BookEntity, BookDto>()
                .ForMember("GenreIds", options =>
                {
                    options.MapFrom<BookIdsResolver, ICollection<BookGenreEntity>>(bookEntity => bookEntity.BookGenres);
                })
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Description));

            CreateMap<BookDto, BookEntity>()
                .ForMember("BookGenres", options =>
                {
                    options.MapFrom<BookGenresResolver, IEnumerable<int>>(bookDto => bookDto.GenreIds);
                })
                .ForMember("Description", options =>
                {
                    options.MapFrom<BookDescriptionResolver, string?>(bookDto => bookDto.Description);
                })
                .ForMember("Warehouse", options =>
                {
                    options.MapFrom<BookWarehouseResolver, WarehouseDto>(bookDto => bookDto.Warehouse);
                });

            CreateMap<WarehouseEntity, WarehouseDto>();
            CreateMap<WarehouseDto, WarehouseViewModel>().ReverseMap();
            CreateMap<WarehouseAddUpdateViewModel, WarehouseDto>();
            
            CreateMap<BookAddViewModel, BookDto>();
            CreateMap<BookUpdateViewModel, BookDto>();


            CreateMap<AuthorEntity, AuthorDto>().ReverseMap();
            CreateMap<AuthorDto, AuthorViewModel>();
            CreateMap<AuthorAddUpdateViewModel, AuthorDto>();
        }
    }
}
