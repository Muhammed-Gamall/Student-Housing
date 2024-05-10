﻿using System.ComponentModel.DataAnnotations.Schema;

namespace sakan.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfBeds { get; set; }
        public int Floor { get; set; }
        public int Price { get; set; }
        public string? Sex { get; set; }
        public string? Time { get; set; }
        public int HouseOwnerID { get; set; }
        [ForeignKey("HouseOwnerID")]
        public HouseOwner HouseOwner { get; set; }
        public List<Student> Student { get; set; }
    }
}
