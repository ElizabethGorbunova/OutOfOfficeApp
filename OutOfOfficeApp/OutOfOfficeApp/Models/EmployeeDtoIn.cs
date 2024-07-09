using OutOfOfficeApp.Enums;

namespace OutOfOfficeApp.Models
{
    public class EmployeeDtoIn
    {
        public string? FullName { get; set; }
        public Subdivision Subdivision { get; set; }
        public Position Position { get; set; }
        public Status Status { get; set; }
        public int PeoplePartner { get; set; }
        public float OutOfOfficeBalance { get; set; }
        public string? Photo { get; set; }
    }
}
