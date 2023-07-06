namespace LibraryManagementSystem.BLL.Models.Entities.StudentEntities
{
    public class StudentEntity
    {
        public int Id { get; init; }
        public string Login { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string? PictureName { get; init; }
        public int CityId { get; init; }
        public CityEntity City { get; init; } = new();
        public string Address { get; init; } = string.Empty;
        public DateTime EntryDate { get; init; }
    }
}
