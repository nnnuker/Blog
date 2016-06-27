namespace DAL.DTO
{
    public class DalBlog : IDalEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}