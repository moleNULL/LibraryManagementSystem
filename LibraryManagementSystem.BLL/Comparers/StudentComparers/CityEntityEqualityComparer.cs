using LibraryManagementSystem.BLL.Models.Entities.StudentEntities;

namespace LibraryManagementSystem.BLL.Comparers.StudentComparers
{
    public class CityEntityEqualityComparer : IEqualityComparer<CityEntity>
    {
        public bool Equals(CityEntity? x, CityEntity? y)
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

        public int GetHashCode(CityEntity obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}