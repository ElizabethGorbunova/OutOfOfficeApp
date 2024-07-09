using OutOfOfficeApp.Enums;

namespace OutOfOfficeApp.Models
{
    public class LeaveRequestDtoIn
    {
        public AbsenceReason AbsenceReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
    }
}
