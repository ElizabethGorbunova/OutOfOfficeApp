using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface ILeaveRequestService
    {
        public IEnumerable<LeaveRequestDtoOut> GetAllLeaveRequests();
        public abstract IEnumerable<LeaveRequestDtoOut> SortLeaveRequests(IEnumerable<LeaveRequestDtoOut> requests, string columnName);
        public abstract IEnumerable<LeaveRequestDtoOut> FilterLeaveRequests(IEnumerable<LeaveRequestDtoOut> requests, string columnName, string value);
        public abstract EditResult<LeaveRequestDtoOut> OpenLeaveRequest(int ID);
        public EditResult<LeaveRequestDtoOut> CreateLeaveRequest(LeaveRequestDtoIn newLeaveRequest);
        public EditResult<LeaveRequestDtoOut> EditLeaveRequest(LeaveRequestDtoIn leaveRequest, int ID);
        public EditResult<LeaveRequestDtoOut> SubmitLeaveRequest(int ID);
        public EditResult<LeaveRequestDtoOut> CancelLeaveRequest(int ID);




    }
}
