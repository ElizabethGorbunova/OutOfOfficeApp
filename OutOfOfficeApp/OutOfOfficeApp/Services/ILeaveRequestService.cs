using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface ILeaveRequestService
    {
        public abstract IEnumerable<LeaveRequestDtoOut> SortLeaveRequests(string columnName);
        public abstract IEnumerable<LeaveRequestDtoOut> FilterLeaveRequests(string columnName, object value);
        public abstract EditResult<LeaveRequestDtoOut> OpenLeaveRequest(int ID);
       
    }
}
