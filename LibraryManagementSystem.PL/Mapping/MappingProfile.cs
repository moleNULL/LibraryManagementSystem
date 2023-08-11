using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.BookDtos;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.AuthorViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.BookViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.GenreViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.LanguageViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.PublisherViewModels;
using LibraryManagementSystem.PL.ViewModels.BookViewModels.WarehouseViewModels;
using LibraryManagementSystem.PL.ViewModels.StudentViewModels.StudentViewModels;

namespace LibraryManagementSystem.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapBookModels();
            MapStudentModels();
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

        private void MapStudentModels()
        {
            MapStudents();
        }
        
        private void MapAuthors()
        {
            CreateMap<AuthorEntity, AuthorDto>().ReverseMap();
            CreateMap<AuthorEntity, AuthorSimpleDto>()
                .ForMember(dest => dest.FullName, options =>
                {
                    options.MapFrom(authorEntity => authorEntity.FirstName + ' ' + authorEntity.LastName);
                });
            CreateMap<AuthorSimpleDto, AuthorSimpleViewModel>();
            CreateMap<AuthorDto, AuthorViewModel>();
            CreateMap<AuthorAddViewModel, AuthorDto>();
            CreateMap<AuthorUpdateViewModel, AuthorDto>();
        }
        private void MapBooks()
        {
            CreateMap<BookDto, BookViewModel>().ReverseMap();

            CreateMap<BookEntity, BookSimpleDto>()
                .ForMember(dest => dest.GenreIds, options =>
                {
                    options.MapFrom(bookEntity => bookEntity.BookGenres.Select(bg => bg.GenreId));
                })
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Description));
            
            CreateMap<BookEntity, BookDto>()
                .ForMember("GenreIds", options =>
                {
                    options.MapFrom<BookGenreIdsResolver, ICollection<BookGenreEntity>>(bookEntity => bookEntity.BookGenres);
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

            CreateMap<BookSimpleDto, BookSimpleViewModel>();
            CreateMap<BookAddViewModel, BookDto>();
            CreateMap<BookUpdateViewModel, BookDto>();
        }
        private void MapGenres()
        {
            CreateMap<GenreEntity, GenreDto>().ReverseMap();
            CreateMap<GenreDto, GenreViewModel>();
            CreateMap<GenreAddViewModel, GenreDto>();
            CreateMap<GenreUpdateViewModel, GenreDto>();
        }
        private void MapLanguages()
        {
            CreateMap<LanguageEntity, LanguageDto>().ReverseMap();
            CreateMap<LanguageDto, LanguageViewModel>();
            CreateMap<LanguageAddViewModel, LanguageDto>();
            CreateMap<LanguageUpdateViewModel, LanguageDto>();
        }
        private void MapPublishers()
        {
            CreateMap<PublisherEntity, PublisherDto>().ReverseMap();
            CreateMap<PublisherDto, PublisherViewModel>();
            CreateMap<PublisherAddViewModel, PublisherDto>();
            CreateMap<PublisherUpdateViewModel, PublisherDto>();
        }
        private void MapWarehouses()
        {
            CreateMap<WarehouseEntity, WarehouseDto>();
            CreateMap<WarehouseDto, WarehouseViewModel>().ReverseMap();
            CreateMap<WarehouseAddViewModel, WarehouseDto>();
            CreateMap<WarehouseUpdateViewModel, WarehouseDto>();
        }


        private void MapStudents()
        {
            CreateMap<StudentEntity, StudentDto>().ForMember("FavoriteGenreIds", options =>
            {
                options.MapFrom<StudentFavoriteGenreIdsResolver, ICollection<StudentGenreEntity>>(studentEntity => studentEntity.StudentGenres);
            });
            
            CreateMap<StudentDto, StudentEntity>()
                .ForMember("StudentGenres", options =>
                {
                    options.MapFrom<StudentGenresResolver, IEnumerable<int>>(studentDto => studentDto.FavoriteGenreIds);
                });
            
            CreateMap<StudentDto, StudentViewModel>();
            CreateMap<StudentAddViewModel, StudentDto>();
            CreateMap<StudentUpdateViewModel, StudentDto>();
        }
    }
}
