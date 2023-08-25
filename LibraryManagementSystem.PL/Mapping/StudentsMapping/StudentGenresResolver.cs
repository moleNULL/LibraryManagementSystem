using AutoMapper;
using LibraryManagementSystem.BLL.Models.Dtos.StudentDtos;
using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.PL.Mapping.StudentsMapping
{
    public class StudentGenresResolver : IMemberValueResolver<StudentDto, StudentEntity, IEnumerable<int>, object>
    {
        public object Resolve(StudentDto source, StudentEntity destination, 
            IEnumerable<int> sourceMember, object destMember, ResolutionContext context)
        {
            return source.FavoriteGenreIds.Select(id => new StudentGenreEntity
            {
                StudentId = source.Id,
                GenreId = id
            });
        }
    }
}