using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers;

public class AuthorEntityComparer : IEqualityComparer<AuthorEntity>
{
    public bool Equals(AuthorEntity? x, AuthorEntity? y)
    {
        if (x!.Id != y!.Id)
        {
            return false;
        }
        
        if (x!.FirstName != y!.FirstName)
        {
            return false;
        }

        if (x!.LastName != y!.LastName)
        {
            return false;
        }

        return true;
    }

    public int GetHashCode(AuthorEntity obj)
    {
        return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName);
    }
}