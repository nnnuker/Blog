namespace BLL.Entities
{
    public class BllTag : IBllEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
    }
}