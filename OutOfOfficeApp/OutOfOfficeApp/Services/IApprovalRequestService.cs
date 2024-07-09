using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IApprovalRequestService
    {
        public IEnumerable<ApprovalRequestDtoOut> GetAllApprovalRequests();
        public abstract IEnumerable<ApprovalRequestDtoOut> SortApprovalRequests(IEnumerable<ApprovalRequestDtoOut> requests, string columnName);
        public abstract IEnumerable<ApprovalRequestDtoOut> FilterApprovalRequests(IEnumerable<ApprovalRequestDtoOut> requests, string columnName, string value);
        public abstract EditResult<ApprovalRequestDtoOut> OpenApprovalRequest(int ID);
        public abstract EditResult<ApprovalRequestDtoOut> ApproveRequest(int ID);
        public abstract EditResult<ApprovalRequestDtoOut> RejectRequest(int ID, string comment="");
    }
}
