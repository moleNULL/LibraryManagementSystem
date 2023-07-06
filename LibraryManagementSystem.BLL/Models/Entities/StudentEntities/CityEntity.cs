namespace LibraryManagementSystem.BLL.Models.Entities.StudentEntities
{
    public class CityEntity
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public ICollection<StudentEntity> Students { get; init; } = new List<StudentEntity>();
    }
}
