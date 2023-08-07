using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.AuthorViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.BookViewModels1;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.GenreViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.LanguageViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.PublisherViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.WarehouseViewModels;

namespace LibraryManagementSystem.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapBookModels();
        }

        private void MapBookModels()
        {
            MapAuthors();
            MapBooks();
            MapGenres();
            MapLanguages();
            MapPublishers();
            MapWarehouses();
        }
        
        private void MapAuthors()
        {
            CreateMap<AuthorEntity, AuthorDto>().ReverseMap();
            CreateMap<AuthorDto, AuthorViewModel>();
            CreateMap<AuthorAddUpdateViewModel, AuthorDto>();
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
            
            CreateMap<BookAddViewModel, BookDto>();
            CreateMap<BookUpdateViewModel, BookDto>();
        }
        private void MapGenres()
        {
            CreateMap<GenreEntity, GenreDto>().ReverseMap();
            CreateMap<GenreDto, GenreViewModel>();
            CreateMap<GenreAddUpdateViewModel, GenreDto>();
        }
        private void MapLanguages()
        {
            CreateMap<LanguageEntity, LanguageDto>().ReverseMap();
            CreateMap<LanguageDto, LanguageViewModel>();
            CreateMap<LanguageAddUpdateViewModel, LanguageDto>();
        }
        private void MapPublishers()
        {
            CreateMap<PublisherEntity, PublisherDto>().ReverseMap();
            CreateMap<PublisherDto, PublisherViewModel>();
            CreateMap<PublisherAddUpdateViewModel, PublisherDto>();
        }
        private void MapWarehouses()
        {
            CreateMap<WarehouseEntity, WarehouseDto>();
            CreateMap<WarehouseDto, WarehouseViewModel>().ReverseMap();
            CreateMap<WarehouseAddUpdateViewModel, WarehouseDto>();
        }
    }
}
