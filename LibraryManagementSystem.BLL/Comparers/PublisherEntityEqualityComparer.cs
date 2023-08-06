using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers;

public class PublisherEntityEqualityComparer : IEqualityComparer<PublisherEntity>
{
    public bool Equals(PublisherEntity? x, PublisherEntity? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
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