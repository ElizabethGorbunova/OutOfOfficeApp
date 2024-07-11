using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class ApprovalRequest
    {
        public int ApprovalRequestId { get; set; }

        [ForeignKey("Employee")]
        public int Approver { get; set; }

        [ForeignKey("LeaveRequest")]
        public int LeaveRequest { get; set; }
        public string? Comment { get; set; }

        public ApprovalRequestStatus Status { get; set; }
    }
}
