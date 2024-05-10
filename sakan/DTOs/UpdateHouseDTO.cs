namespace sakan.DTOs
{
    public class UpdateHouseDTO
    {
        public string Location { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
        public int? NumOfRooms { get; set; }
        public int? NumOfBeds { get; set; }
        public int? Floor { get; set; }
        public int? Price { get; set; }

    }
}
