using System.ComponentModel.DataAnnotations.Schema;

namespace sakan.Models
{
    public class Student
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Sex { set; get; }
        public string faculty { set; get; }
        public string Governorate { set; get; }
        public int Level { set; get; }
        public byte[]? Photo { get; set; }
        public int? HouseNum { set; get; }
        public int? HouseID { set; get; }
        public House House { set; get; }
    }
}
