using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? FullName { get; set; }
        public Subdivision Subdivision { get; set; }
        public Position Position { get; set; }
        public Status Status { get; set; }

        [ForeignKey("EmployeeID")]
        public int PeoplePartner { get; set; }      //DT Change
        public float OutOfOfficeBalance { get; set; }
        public string? Photo { get; set; }
        public virtual List<Project>? Projects { get; set; }

    }
}
