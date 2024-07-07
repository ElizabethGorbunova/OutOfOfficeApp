using AutoMapper;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.Linq.Dynamic.Core;

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
        public IEnumerable<LeaveRequestDtoOut> SortLeaveRequests(string columnName)
        {
            string query = $"{columnName} == @0";
            var sortedLeaveRequests = dbContext.LeaveRequests.OrderBy(query).ToList();
            var leaveRequestsDtoOut = mapper.Map<IEnumerable<LeaveRequestDtoOut>>(sortedLeaveRequests);
            return leaveRequestsDtoOut;
        }
        public IEnumerable<LeaveRequestDtoOut> FilterLeaveRequests(string columnName, object value)
        {
            string query = $"{columnName} == @0";
            var filteredLeaveRequests = dbContext.LeaveRequests.Where(query, value).ToList();
            var leaveRequestsDtoOut = mapper.Map<IEnumerable<LeaveRequestDtoOut>>(filteredLeaveRequests);
            return leaveRequestsDtoOut;
        }
        public EditResult<LeaveRequestDtoOut> OpenLeaveRequest(int ID)
        {
            var leaveRequestToOpen = dbContext.LeaveRequests.FirstOrDefault(e => e.LeaveRequestID == ID);

            if (leaveRequestToOpen == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToOpen);

            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> CreateLeaveRequest(LeaveRequest newLeaveRequest)
        {
            newLeaveRequest.Status = Enums.LeaveRequestStatus.New;
            dbContext.LeaveRequests.Add(newLeaveRequest);
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(newLeaveRequest);

            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> EditLeaveRequest(LeaveRequest leaveRequest, int ID)
        {
            var leaveRequestToEdit = dbContext.LeaveRequests.FirstOrDefault(e => e.LeaveRequestID == ID);

            if (leaveRequestToEdit == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            leaveRequestToEdit.AbsenceReason = leaveRequest.AbsenceReason;
            leaveRequestToEdit.StartDate = leaveRequest.StartDate;
            leaveRequestToEdit.EndDate = leaveRequest.EndDate;
            leaveRequestToEdit.Comment = leaveRequest.Comment;
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToEdit);
            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> SubmitLeaveRequest(int ID)
        {
            var leaveRequestToSubmit = dbContext.LeaveRequests.FirstOrDefault(e => e.LeaveRequestID == ID);

            if (leaveRequestToSubmit == null)
            {
                return new EditResult<LeaveRequestDtoOut>() { IsSuccess = false, Model = null };
            }
            leaveRequestToSubmit.Status = Enums.LeaveRequestStatus.Submit;
            dbContext.SaveChanges();

            var leaveRequestDtoOut = mapper.Map<LeaveRequestDtoOut>(leaveRequestToSubmit);
            return new EditResult<LeaveRequestDtoOut>() { IsSuccess = true, Model = leaveRequestDtoOut };
        }
        public EditResult<LeaveRequestDtoOut> CancelLeaveRequest(int ID)
        {
            var leaveRequestToCancel = dbContext.LeaveRequests.FirstOrDefault(e => e.LeaveRequestID == ID);

            if (leaveRequestToCancel == null)
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
