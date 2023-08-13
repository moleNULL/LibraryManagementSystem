using LibraryManagementSystem.BLL.Models.Entities;

namespace LibraryManagementSystem.BLL.Comparers.LibraryComparers;

public class LibraryEqualityEntityComparer : IEqualityComparer<LibrarianEntity>
{
    public bool Equals(LibrarianEntity? x, LibrarianEntity? y)
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
               x.PictureName == y.PictureName &&
               x.EntryDate.Equals(y.EntryDate);
    }

    public int GetHashCode(LibrarianEntity obj)
    {
        return HashCode.Combine(
            obj.Id, 
            obj.FirstName, 
            obj.LastName, 
            obj.Email, 
            obj.PictureName, 
            obj.EntryDate);
    }
}