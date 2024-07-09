using OutOfOfficeApp.Enums;

namespace OutOfOfficeApp.Models
{
    public class ProjectDtoIn
    {
        public ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectManager { get; set; }
        public string? Comment { get; set; }
    }
}
