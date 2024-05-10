namespace sakan.DTOs
{
    public class CreateHouseDTO
    {
        public string Location { get; set; }
        public string? Description { get; set; }
        public string? Sex { get; set; }
        public IFormFile? Photo { get; set; }
        public int? NumOfRooms { get; set; }
        public int? NumOfBeds { get; set; }
        public int? Floor { get; set; }
        public int? Price { get; set; }
        public string? Time { get; set; }


        //  public int HouseOwnerID { get; set; }


    }
}
