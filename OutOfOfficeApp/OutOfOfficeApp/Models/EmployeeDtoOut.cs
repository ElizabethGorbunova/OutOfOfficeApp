using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeApp.Models
{
    public class EmployeeDtoOut
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public Subdivision Subdivision { get; set; }
        public Position Position { get; set; }
        public Status Status { get; set; }
        public int PeoplePartnerId { get; set; }
        public float OutOfOfficeBalance { get; set; }
        public string? Photo { get; set; }
    }
}
