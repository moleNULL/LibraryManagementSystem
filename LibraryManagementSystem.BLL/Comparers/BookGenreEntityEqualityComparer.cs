using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers
{
    public class BookGenreEntityEqualityComparer : IEqualityComparer<BookGenreEntity>
    {
        public bool Equals(BookGenreEntity? x, BookGenreEntity? y)
        {
            if (x!.BookId != y!.BookId)
            {
                return false;
            }

            if (x!.GenreId != y!.GenreId)
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(BookGenreEntity obj)
        {
            return HashCode.Combine(obj.BookId, obj.GenreId);
        }
    }
}
