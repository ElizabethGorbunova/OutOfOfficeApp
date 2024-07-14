using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
        public int ProjectManager { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }
        public virtual List<EmployeeProject> EmployeeProjects { get; set; }

    }
}
