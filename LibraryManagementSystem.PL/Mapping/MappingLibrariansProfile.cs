using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos;
using LibraryManagementSystem.BLL.Models.Entities;
using LibraryManagementSystem.PL.ViewModels.LibrarianViewModels.LibrarianViewModels;

namespace LibraryManagementSystem.PL.Mapping
{
    public class MappingLibrariansProfile : Profile
    {
        public MappingLibrariansProfile()
        {
            MapLibrarians();
        }
    
        private void MapLibrarians()
        {
            CreateMap<LibrarianEntity, LibrarianDto>().ReverseMap();
            CreateMap<LibrarianDto, LibrarianViewModel>();
            CreateMap<LibrarianAddViewModel, LibrarianDto>();
            CreateMap<LibrarianUpdateViewModel, LibrarianDto>();
        }
    }
}