namespace DAL.DTO
{
    public class DalTag : IDalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
    }
}