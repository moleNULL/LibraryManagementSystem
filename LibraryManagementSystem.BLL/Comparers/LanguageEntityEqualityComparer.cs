using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers;

public class LanguageEntityEqualityComparer : IEqualityComparer<LanguageEntity>
{
    public bool Equals(LanguageEntity? x, LanguageEntity? y)
    {
        if (x!.Id != y!.Id)
        {
            return false;
        }

        if (x!.Name != y!.Name)
        {
            return false;
        }

        return true;
    }

    public int GetHashCode(LanguageEntity obj)
    {
        return HashCode.Combine(obj.Id, obj.Name);
    }
}