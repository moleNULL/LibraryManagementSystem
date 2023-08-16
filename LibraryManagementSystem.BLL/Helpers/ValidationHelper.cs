namespace LibraryManagementSystem.BLL.Helpers
{
    public static class ValidationHelper
    {
        public static void ValidateId(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id cannot be negative or zero");
            }
        }
    }
}