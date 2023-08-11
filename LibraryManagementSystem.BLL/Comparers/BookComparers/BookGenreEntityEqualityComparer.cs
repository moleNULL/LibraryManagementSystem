using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers.BookComparers
{
    public class BookGenreEntityEqualityComparer : IEqualityComparer<BookGenreEntity>
    {
        public bool Equals(BookGenreEntity? x, BookGenreEntity? y)
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

            return x.BookId == y.BookId && 
                   x.GenreId == y.GenreId;
        }

        public int GetHashCode(BookGenreEntity obj)
        {
            return HashCode.Combine(obj.BookId, obj.GenreId);
        }
    }
}
