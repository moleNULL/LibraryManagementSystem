using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers;

public class AuthorEntityComparer : IEqualityComparer<AuthorEntity>
{
    public bool Equals(AuthorEntity? x, AuthorEntity? y)
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

        return x.Id == y.Id && x.FirstName == y.FirstName && x.LastName == y.LastName;
    }

    public int GetHashCode(AuthorEntity obj)
    {
        return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName);
    }
}