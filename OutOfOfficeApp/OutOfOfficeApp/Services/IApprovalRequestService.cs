using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IApprovalRequestService
    {
        public abstract IEnumerable<ApprovalRequestDtoOut> SortApprovalRequests(string columnName);
        public abstract IEnumerable<ApprovalRequestDtoOut> FilterApprovalRequests(string columnName, object value);
        public abstract EditResult<ApprovalRequestDtoOut> OpenApprovalRequest(int ID);
        public abstract EditResult<ApprovalRequestDtoOut> ApproveRequest(int ID);
        public abstract EditResult<ApprovalRequestDtoOut> RejectRequest(int ID, string comment="");
    }
}
