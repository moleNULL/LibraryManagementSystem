namespace LibraryManagementSystem.BLL.Models.Entities.StudentEntities
{
    public class CityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
    }
}
