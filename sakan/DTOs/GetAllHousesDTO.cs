namespace sakan.DTOs
{
    public class GetAllHousesDTO
    {
        public int Id { get; set; }
        public byte[]? Photo { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public string? Time { get; set; }

    }
}
