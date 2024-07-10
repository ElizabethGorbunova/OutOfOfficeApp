using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface ILeaveRequestService
    {
        public IEnumerable<LeaveRequestDtoOut> GetAllLeaveRequests();
        public IEnumerable<LeaveRequestDtoOut> GetAllLeaveRequestsForCurrentEmployee(int id);
        public abstract IEnumerable<LeaveRequestDtoOut> SortLeaveRequests(IEnumerable<LeaveRequestDtoOut> requests, string columnName);
        public abstract IEnumerable<LeaveRequestDtoOut> FilterLeaveRequests(IEnumerable<LeaveRequestDtoOut> requests, string columnName, string value);
        public abstract EditResult<LeaveRequestDtoOut> OpenLeaveRequest(int leaveRequestId, int userId);
        public EditResult<LeaveRequestDtoOut> CreateLeaveRequest(LeaveRequestDtoIn newLeaveRequest, int userId);
        public EditResult<LeaveRequestDtoOut> EditLeaveRequest(LeaveRequestDtoIn leaveRequest, int leaveRequestId, int userId);
        public EditResult<LeaveRequestDtoOut> SubmitLeaveRequest(int leaveRequestId, int userId);
        public EditResult<LeaveRequestDtoOut> CancelLeaveRequest(int leaveRequestId, int userId);




    }
}
