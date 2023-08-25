using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Comparers.StudentComparers
{
    public class StudentEntityEqualityComparer : IEqualityComparer<StudentEntity>
    {
        public bool Equals(StudentEntity? x, StudentEntity? y)
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

            return x.Id == y.Id &&
                   x.FirstName == y.FirstName &&
                   x.LastName == y.LastName &&
                   x.Email == y.Email &&
                   x.StudentGenres.SequenceEqual(y.StudentGenres, new StudentGenreEntityEqualityComparer()) &&
                   x.PictureName == y.PictureName &&
                   x.CityId == y.CityId &&
                   x.Address == y.Address &&
                   x.EntryDate.Equals(y.EntryDate);
        }

        public int GetHashCode(StudentEntity obj)
        {
            var hashCode = new HashCode();
        
            hashCode.Add(obj.Id);
            hashCode.Add(obj.FirstName);
            hashCode.Add(obj.LastName);
            hashCode.Add(obj.Email);
            hashCode.Add(obj.PictureName);
            hashCode.Add(obj.CityId);
            hashCode.Add(obj.Address);
            hashCode.Add(obj.EntryDate);
        
            if (obj.StudentGenres is not null)
            {
                foreach (var genre in obj.StudentGenres)
                {
                    hashCode.Add(genre.StudentId);
                    hashCode.Add(genre.GenreId);
                }
            }
        
            return hashCode.ToHashCode();
        }
    }
}