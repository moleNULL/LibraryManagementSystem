using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers
{
    public class BookEntityEqualityComparer : IEqualityComparer<BookEntity>
    {
        public bool Equals(BookEntity? x, BookEntity? y)
        {
            if (x!.Title != y!.Title)
            {
                return false;
            }

            if (x!.PictureName != y!.PictureName)
            {
                return false;
            }

            if (x!.PagesNumber != y!.PagesNumber)
            {
                return false;
            }

            if (x!.Year != y!.Year)
            {
                return false;
            }

            if (x!.PublisherId != y!.PublisherId)
            {
                return false;
            }

            if (x!.AuthorId != y!.AuthorId)
            {
                return false;
            }

            if (x!.DescriptionId != y!.DescriptionId)
            {
                return false;
            }

            if (x!.WarehouseId != y!.WarehouseId)
            {
                return false;
            }

            if (x!.LanguageId != y!.LanguageId)
            {
                return false;
            }

            if (x!.BookLoanId != y!.BookLoanId)
            {
                return false;
            }

            if (!Enumerable.SequenceEqual(x!.Genres, y!.Genres, new GenreEntityEqualityComparer()))
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(BookEntity obj)
        {
            return base.GetHashCode();
        }
    }
}
