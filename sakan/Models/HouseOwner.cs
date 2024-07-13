namespace sakan.Models
{
    public class HouseOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { set; get; }
        public string Password { set; get; }
        public List<House> Houses { set; get; }
    }
}
