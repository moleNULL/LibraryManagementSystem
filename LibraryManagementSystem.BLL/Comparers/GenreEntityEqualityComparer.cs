using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers
{
    public class GenreEntityEqualityComparer : IEqualityComparer<GenreEntity>
    {
        public bool Equals(GenreEntity? x, GenreEntity? y)
        {
            if (x!.Id != y!.Id)
            {
                return false;
            }

            if (x!.Name != y!.Name)
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(GenreEntity obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}
