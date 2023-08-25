using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;
using LibraryManagementSystem.PL.Mapping.StudentsMapping;
using LibraryManagementSystem.PL.ViewModels.StudentViewModels.CityViewModels;
using LibraryManagementSystem.PL.ViewModels.StudentViewModels.StudentViewModels;

namespace LibraryManagementSystem.PL.Mapping
{
    public class MappingStudentsProfile : Profile
    {
        public MappingStudentsProfile()
        {
            MapStudents();
            MapCities();
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

        private void MapCities()
        {
            CreateMap<CityEntity, CityDto>().ReverseMap();
            CreateMap<CityDto, CityViewModel>();
            CreateMap<CityAddViewModel, CityDto>();
            CreateMap<CityUpdateViewModel, CityDto>();
        }
    }
}