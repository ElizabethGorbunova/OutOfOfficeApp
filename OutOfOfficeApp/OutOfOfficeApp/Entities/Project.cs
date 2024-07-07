using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("EmployeeID")]
        public int ProjectManager { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }
        public virtual List<Employee>? Employees { get; set; }

    }
}
