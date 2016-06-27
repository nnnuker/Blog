namespace BLL.Entities
{
    public class BllBlog : IBllEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}