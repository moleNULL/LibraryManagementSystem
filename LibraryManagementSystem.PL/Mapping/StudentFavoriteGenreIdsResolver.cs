using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.BookEntities;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.PL.Mapping;


public class StudentFavoriteGenreIdsResolver : IMemberValueResolver<StudentEntity, StudentDto, ICollection<StudentGenreEntity>, object>
{
    public object Resolve(StudentEntity source, StudentDto destination, 
        ICollection<StudentGenreEntity> sourceMember, object destMember,
        ResolutionContext context)
    {
        return source.StudentGenres.Select(sg => sg.GenreId);
    }
}