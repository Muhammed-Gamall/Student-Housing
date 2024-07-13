namespace sakan.DTOs
{
    public class GetHousesDTO
    {
        public string Location { get; set; }
        public string? Description { get; set; }
        public string? Photo1 { get; set; }
        public string? Photo2 { get; set; }
        public string? Photo3 { get; set; }
        public string? Photo4 { get; set; }
        public int NmOfRooms { get; set; }
        public int NumOfBeds { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }
        public int phone { get; set; }


    }
}
