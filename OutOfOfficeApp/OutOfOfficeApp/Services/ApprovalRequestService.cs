using AutoMapper;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.Linq.Dynamic.Core;

namespace OutOfOfficeApp.Services
{
    public class ApprovalRequestService : IApprovalRequestService
    {

        private readonly OOODbContext dbContext;
        private readonly IMapper mapper;

        public ApprovalRequestService(OOODbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public IEnumerable<ApprovalRequestDtoOut> SortApprovalRequests(string columnName)
        {
            string query = $"{columnName} == @0";
            var sortedApprovalRequests = dbContext.ApprovalRequests.OrderBy(query).ToList();
            var approvalRequestsDtoOut = mapper.Map<IEnumerable<ApprovalRequestDtoOut>>(sortedApprovalRequests);
            return approvalRequestsDtoOut;
        }

        public IEnumerable<ApprovalRequestDtoOut> FilterApprovalRequests(string columnName, object value)
        {
            string query = $"{columnName} == @0";
            var filteredApprovalRequests = dbContext.ApprovalRequests.Where(query, value).ToList();
            var approvalRequestsDtoOut = mapper.Map<IEnumerable<ApprovalRequestDtoOut>>(filteredApprovalRequests);
            return approvalRequestsDtoOut;
        }

        public EditResult<ApprovalRequestDtoOut> OpenApprovalRequest(int ID)
        {
            var approvalRequestToOpen = dbContext.ApprovalRequests.FirstOrDefault(e => e.ApprovalRequestID == ID);

            if (approvalRequestToOpen == null)
            {
                return new EditResult<ApprovalRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            var approvalRequestDtoOut = mapper.Map<ApprovalRequestDtoOut>(approvalRequestToOpen);

            return new EditResult<ApprovalRequestDtoOut>() {IsSuccess = true, Model = approvalRequestDtoOut};
        }

        public EditResult<ApprovalRequestDtoOut> ApproveRequest(int ID)
        {
            var approvalRequestToApprove = dbContext.ApprovalRequests.FirstOrDefault(e => e.ApprovalRequestID == ID);

            if (approvalRequestToApprove == null)
            {
                return new EditResult<ApprovalRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            approvalRequestToApprove.Status = Enums.ApprovalRequestStatus.Approve;
            dbContext.SaveChanges();

            var approvalRequestDtoOut = mapper.Map<ApprovalRequestDtoOut>(approvalRequestToApprove);

            return new EditResult<ApprovalRequestDtoOut>() { IsSuccess = true, Model = approvalRequestDtoOut };
        }

        public EditResult<ApprovalRequestDtoOut> RejectRequest(int ID, string comment = "")
        {
            var approvalRequestToReject = dbContext.ApprovalRequests.FirstOrDefault(e => e.ApprovalRequestID == ID);

            if (approvalRequestToReject == null)
            {
                return new EditResult<ApprovalRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            approvalRequestToReject.Status = Enums.ApprovalRequestStatus.Reject;
            if (comment != "")
            {
                approvalRequestToReject.Comment = comment;
            }
            dbContext.SaveChanges();

            var approvalRequestDtoOut = mapper.Map<ApprovalRequestDtoOut>(approvalRequestToReject);

            return new EditResult<ApprovalRequestDtoOut>() { IsSuccess = true, Model = approvalRequestDtoOut };
        }

        
    }
}
