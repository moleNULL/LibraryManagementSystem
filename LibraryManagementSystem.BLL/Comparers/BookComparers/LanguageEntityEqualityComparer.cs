using LibraryManagementSystem.BLL.Models.Entities.BookEntities;

namespace LibraryManagementSystem.BLL.Comparers.BookComparers
{
    public class LanguageEntityEqualityComparer : IEqualityComparer<LanguageEntity>
    {
        public bool Equals(LanguageEntity? x, LanguageEntity? y)
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
                   x.Name == y.Name;
        }

        public int GetHashCode(LanguageEntity obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}