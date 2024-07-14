using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeApp.Models
{
    public class ProjectDtoOut
    {
        public int Id { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectManager { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }
    }
}
