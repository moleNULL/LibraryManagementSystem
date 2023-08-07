using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers;

public class PublisherEntityEqualityComparer : IEqualityComparer<PublisherEntity>
{
    public bool Equals(PublisherEntity? x, PublisherEntity? y)
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
        
        return x.Id == y.Id && x.Name == y.Name;
    }

    public int GetHashCode(PublisherEntity obj)
    {
        return HashCode.Combine(obj.Id, obj.Name);
    }
}