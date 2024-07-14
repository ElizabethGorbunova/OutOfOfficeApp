using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public Subdivision Subdivision { get; set; }
        public Position Position { get; set; }
        public Status Status { get; set; }
        public virtual Employee? PeoplePartner { get; set; }
        public int PeoplePartnerId { get; set; }   
        public float OutOfOfficeBalance { get; set; }
        public string? Photo { get; set; }
        public virtual List<Project> Projects { get; set; } = new List<Project>();

        public virtual List<EmployeeProject>? EmployeeProjects { get; set; }

        public virtual User? User { get; set; }
        public int UserId { get; set; }

        public List<ApprovalRequest>? ApprovalRequests { get; set; }
        public List<LeaveRequest>? LeaveRequests { get; set; }

    }
}
