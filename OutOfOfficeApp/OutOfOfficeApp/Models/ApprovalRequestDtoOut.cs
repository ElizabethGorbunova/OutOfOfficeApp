using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeApp.Models
{
    public class ApprovalRequestDtoOut
    {

        public int Id { get; set; }
        public int Approver { get; set; }
        public int LeaveRequest { get; set; }
        public string? Comment { get; set; }
        public ApprovalRequestStatus Status { get; set; }
    }
}
