using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace OutOfOfficeApp.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {

        private readonly OOODbContext dbContext;
        private readonly IMapper mapper;

        public LeaveRequestService(OOODbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }
        public IEnumerable<LeaveRequestDtoOut> GetAllLeaveRequests()
        {
            var allRequests = dbContext.LeaveRequests.ToList();
            var requestsDtoOut = mapper.Map<IEnumerable<LeaveRequestDtoOut>>(allRequests);
            return requestsDtoOut;
        }
        public IEnumerable<LeaveRequestDtoOut> GetAllLeaveRequestsForCurrentEmployee(int userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id== userId);
            var employeeId = user.EmployeeId;
            var requestsOfCurrentEmployee = dbContext.LeaveRequests.Where(x => x.EmployeeId == employeeId).ToList();
            var requestsDtoOut = mapper.Map<IEnumerable<LeaveRequestDtoOut>>(requestsOfCurrentEmployee);

            return requestsDtoOut;
        }
        public IEnumerable<LeaveRequestDtoOut> SortLeaveRequests(IEnumerable<LeaveRequestDtoOut> requests, string columnName)
        {
            string query = $"{columnName}";
            var sortedRequests = requests.AsQueryable().OrderBy(query).ToList();
            var requestsDtoOut = mapper.Map<IEnumerable<LeaveRequestDtoOut>>(sortedRequests);
            return requestsDtoOut;
        }
        public IEnumerable<LeaveRequestDtoOut> FilterLeaveRequests(IEnumerable<LeaveRequestDtoOut> requests, string columnName, string value)
        {
            string query = $"{columnName}==@0";

            var column = typeof(LeaveRequest).GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var columnType = Nullable.GetUnderlyingType(column.PropertyType) ?? column.PropertyType;
            object convertedValue;

            if (columnType.IsEnum)
            {
                convertedValue = Enum.Parse(columnType, value, ignoreCase: true);
            }
            else
            {
                convertedValue = Convert.ChangeType(value, columnType);
            }

            var filteredRequests = requests.AsQueryable().Where(query, convertedValue).ToList();

            var requestsDtoOut = mapper.Map<IEnumerable<LeaveRequestDtoOut>>(filteredRequests);
            return requestsDtoOut;
        }
        public EditResult<LeaveRequestDtoOut> OpenLeaveRequest(int leaveRequestId, int userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var employeeId = user.EmployeeId;

            var leaveRequestToOpen = dbContext.LeaveRequests.FirstOrDefault(e => e.Id == leaveRequestId);

            if (leaveRequestToOpen == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            if (!(leaveRequestToOpen.EmployeeId == employeeId))
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToOpen);

            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> CreateLeaveRequest(LeaveRequestDtoIn newLeaveRequest, int userId)
        {
            var requestMapped = mapper.Map<LeaveRequest>(newLeaveRequest);
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var employeeId = user.EmployeeId;

            requestMapped.EmployeeId = employeeId;
            requestMapped.Status = Enums.LeaveRequestStatus.New;
            dbContext.LeaveRequests.Add(requestMapped);
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(requestMapped);

            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> EditLeaveRequest(LeaveRequestDtoIn leaveRequest, int leaveRequestId, int userId)
        {
            var requestMapped = mapper.Map<LeaveRequest>(leaveRequest);

            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var employeeId = user.EmployeeId;

            var leaveRequestToEdit = dbContext.LeaveRequests.FirstOrDefault(e => e.Id == leaveRequestId);

            if (leaveRequestToEdit == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            if (!(leaveRequestToEdit.EmployeeId == employeeId))
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            leaveRequestToEdit.AbsenceReason = requestMapped.AbsenceReason;
            leaveRequestToEdit.StartDate = requestMapped.StartDate;
            leaveRequestToEdit.EndDate = requestMapped.EndDate;
            leaveRequestToEdit.Comment = requestMapped.Comment;
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToEdit);
            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> SubmitLeaveRequest(int leaveRequestId, int userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var employeeId = user.EmployeeId;

            var leaveRequestToSubmit = dbContext.LeaveRequests.FirstOrDefault(e => e.Id == leaveRequestId);

            if (leaveRequestToSubmit == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            if (!(leaveRequestToSubmit.EmployeeId == employeeId))
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            leaveRequestToSubmit.Status = Enums.LeaveRequestStatus.Submit;
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToSubmit);
            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> CancelLeaveRequest(int leaveRequestId, int userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var employeeId = user.EmployeeId;

            var leaveRequestToCancel = dbContext.LeaveRequests.FirstOrDefault(e => e.Id == leaveRequestId);

            if (leaveRequestToCancel == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            if (!(leaveRequestToCancel.EmployeeId == employeeId))
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            leaveRequestToCancel.Status = Enums.LeaveRequestStatus.Cancel;
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToCancel);
            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }

        
    }
}
