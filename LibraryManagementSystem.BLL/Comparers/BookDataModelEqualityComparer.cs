using LibraryManagementSystem.BLL.Models.DataModels;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagementSystem.BLL.Comparers
{
    public class BookDataModelEqualityComparer : IEqualityComparer<BookDataModel>
    {
        public bool Equals(BookDataModel? x, BookDataModel? y)
        {
            if (x!.Id != y!.Id)
            {
                return false;
            }

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

            if (!Enumerable.SequenceEqual(x!.GenreIds, y!.GenreIds))
            {
                return false;
            }

            if (x!.BookLoanId != y!.BookLoanId)
            {
                return false;
            }

            return true;
        }

        public int GetHashCode([DisallowNull] BookDataModel obj)
        {
            return base.GetHashCode();
        }
    }
}
