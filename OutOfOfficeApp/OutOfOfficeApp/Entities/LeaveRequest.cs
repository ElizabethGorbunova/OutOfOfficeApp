using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
        public AbsenceReason AbsenceReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
        public LeaveRequestStatus Status { get; set; }
    }
}
