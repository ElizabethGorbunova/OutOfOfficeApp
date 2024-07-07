using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class ApprovalRequest
    {
        public int ApprovalRequestID { get; set; }

        [ForeignKey("EmployeeID")]
        public int Approver { get; set; }

        [ForeignKey("LeaveRequestID")]
        public int LeaveRequest { get; set; }
        public string? Comment { get; set; }
        public ApprovalRequestStatus Status { get; set; }
    }
}
