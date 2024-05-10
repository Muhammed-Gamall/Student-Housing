namespace sakan.DTOs
{
    public class GetHousesDTO
    {
        public string Location { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public int NmOfRooms { get; set; }
        public int NumOfBeds { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }

    }
}
