namespace sakan.DTOs
{
    public class CreateStudentDTO
    {
        public string? Name { set; get; }
        public string? Email { set; get; }
        public string? Password { set; get; }
        public string? Sex { set; get; }
        public string? faculty { set; get; }
        public string? Governorate { set; get; }
        public int? Level { set; get; }
        public IFormFile? Photo  { set; get; }
}
}
