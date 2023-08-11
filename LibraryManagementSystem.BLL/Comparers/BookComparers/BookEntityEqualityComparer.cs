using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers.BookComparers
{
    public class BookEntityEqualityComparer : IEqualityComparer<BookEntity>
    {
        public bool Equals(BookEntity? x, BookEntity? y)
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
                   x.Title == y.Title &&
                   x.PictureName == y.PictureName &&
                   x.PagesNumber == y.PagesNumber &&
                   x.Year == y.Year &&
                   x.PublisherId == y.PublisherId &&
                   x.AuthorId == y.AuthorId &&
                   x.DescriptionId == y.DescriptionId &&
                   x.WarehouseId == y.WarehouseId &&
                   x.LanguageId == y.LanguageId &&
                   Enumerable.SequenceEqual(x.BookGenres, y.BookGenres, new BookGenreEntityEqualityComparer()) &&
                   x.BookLoanId == y.BookLoanId;
        }

        public int GetHashCode(BookEntity obj)
        {
            var hashCode = new HashCode();
            
            hashCode.Add(obj.Id);
            hashCode.Add(obj.Title);
            hashCode.Add(obj.PictureName);
            hashCode.Add(obj.PagesNumber);
            hashCode.Add(obj.Year);
            hashCode.Add(obj.PublisherId);
            hashCode.Add(obj.AuthorId);
            hashCode.Add(obj.DescriptionId ?? 0);
            hashCode.Add(obj.WarehouseId);
            hashCode.Add(obj.LanguageId);
            
            if (obj.BookGenres is not null)
            {
                foreach (var genre in obj.BookGenres)
                {
                    hashCode.Add(genre.BookId);
                    hashCode.Add(genre.GenreId);
                }
            }

            hashCode.Add(obj.BookLoanId ?? 0);
            
            return hashCode.ToHashCode();
        }
    }
}
