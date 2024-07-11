using AutoMapper;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;

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
        public IEnumerable<ApprovalRequestDtoOut> GetAllApprovalRequests()
        {
            var allRequests = dbContext.ApprovalRequests.ToList();
            var requestsDtoOut = mapper.Map<IEnumerable<ApprovalRequestDtoOut>>(allRequests);
            return requestsDtoOut;
        }
        public IEnumerable<ApprovalRequestDtoOut> SortApprovalRequests(IEnumerable<ApprovalRequestDtoOut> requests, string columnName)
        {
            string query = $"{columnName}";
            var sortedRequests = requests.AsQueryable().OrderBy(query).ToList();
            var requestsDtoOut = mapper.Map<IEnumerable<ApprovalRequestDtoOut>>(sortedRequests);
            return requestsDtoOut;
        }
        public IEnumerable<ApprovalRequestDtoOut> FilterApprovalRequests(IEnumerable<ApprovalRequestDtoOut> requests, string columnName, string value)
        {
            string query = $"{columnName}==@0";

            var column = typeof(ApprovalRequest).GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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

            var requestsDtoOut = mapper.Map<IEnumerable<ApprovalRequestDtoOut>>(filteredRequests);
            return requestsDtoOut;
        }
        public EditResult<ApprovalRequestDtoOut> OpenApprovalRequest(int ID)
        {
            var approvalRequestToOpen = dbContext.ApprovalRequests.FirstOrDefault(e => e.ApprovalRequestId == ID);

            if (approvalRequestToOpen == null)
            {
                return new EditResult<ApprovalRequestDtoOut>() { IsSuccess = false, Model = null };
            }

            var approvalRequestDtoOut = mapper.Map<ApprovalRequestDtoOut>(approvalRequestToOpen);

            return new EditResult<ApprovalRequestDtoOut>() {IsSuccess = true, Model = approvalRequestDtoOut};
        }
        public EditResult<ApprovalRequestDtoOut> ApproveRequest(int ID)
        {
            var approvalRequestToApprove = dbContext.ApprovalRequests.FirstOrDefault(e => e.ApprovalRequestId == ID);

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
            var approvalRequestToReject = dbContext.ApprovalRequests.FirstOrDefault(e => e.ApprovalRequestId == ID);

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
