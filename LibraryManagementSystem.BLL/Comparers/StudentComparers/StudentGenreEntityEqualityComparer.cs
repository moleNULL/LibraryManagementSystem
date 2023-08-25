using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Comparers.StudentComparers
{
    public class StudentGenreEntityEqualityComparer : IEqualityComparer<StudentGenreEntity>
    {
        public bool Equals(StudentGenreEntity? x, StudentGenreEntity? y)
        {
            if (x == y)
            {
                return true;
            }

            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            return x.StudentId == y.StudentId && 
                   x.GenreId == y.GenreId;
        }

        public int GetHashCode(StudentGenreEntity obj)
        {
            return HashCode.Combine(obj.StudentId, obj.GenreId);
        }
    }
}