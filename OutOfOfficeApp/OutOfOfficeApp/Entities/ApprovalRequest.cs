using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace OutOfOfficeApp.Entities
{
    public class ApprovalRequest
    {
        public int Id { get; set; }
        public virtual Employee? Approver { get; set; }
        public int? ApproverId { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
        public int LeaveRequestId { get; set; }
        public string? Comment { get; set; }
        public ApprovalRequestStatus Status { get; set; }
    }
}
