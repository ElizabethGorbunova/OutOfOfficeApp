using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Enums;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/approvalRequests")]
    [Authorize]
    public class ApprovalRequestController:ControllerBase
    {
        
        public readonly OOODbContext dbContext;
        public readonly IApprovalRequestService approvalService;
        private readonly IMapper mapper;
        public ApprovalRequestController(OOODbContext _dbContext, IApprovalRequestService _approvalService, IMapper _mapper)
        {
            dbContext = _dbContext;
            approvalService = _approvalService;
            mapper = _mapper;
        }

        [Authorize(Roles = "HRManager, ProjectManager, Administrator")]
        [HttpGet()]
        public ActionResult<IEnumerable<ApprovalRequestDtoOut>> GetSortedOrFilteredApprovalRequests([FromQuery(Name = "sortBy")] string? columnNameToSortBy = null, [FromQuery(Name = "filter")] Dictionary<string, string>? filterParams = null)
        {

            IEnumerable<ApprovalRequestDtoOut> sortedFilteredRequests = approvalService.GetAllApprovalRequests();

            if (!string.IsNullOrEmpty(columnNameToSortBy))
            {
                sortedFilteredRequests = approvalService.SortApprovalRequests(sortedFilteredRequests, columnNameToSortBy);
                if (sortedFilteredRequests == null)
                {
                    return NotFound("No requests found with the given criteria.");
                }
            }

            if (filterParams != null)
            {
                foreach (var pair in filterParams)
                {
                    sortedFilteredRequests = approvalService.FilterApprovalRequests(sortedFilteredRequests, filterParams["columnName"], filterParams["value"]);
                    if (sortedFilteredRequests == null)
                    {
                        return NotFound("No requests found with the given criteria.");
                    }
                }

            }

            return Ok(sortedFilteredRequests);

        }

        [Authorize(Roles = "HRManager, ProjectManager, Administrator")]
        [HttpGet("{approvalRequestId}")]
        public ActionResult<ApprovalRequestDtoOut> OpenApprovalRequest(int approvalRequestId)
        {
            var openedRequest = approvalService.OpenApprovalRequest(approvalRequestId);
            if (openedRequest.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(openedRequest.Model);

        }

        [Authorize(Roles = "HRManager, ProjectManager, Administrator")]
        [HttpPut("{approvalRequestId}")]
        public ActionResult<ApprovalRequestDtoOut> UpdateApprovalRequest(int approvalRequestId, [FromQuery(Name = "status")] ApprovalRequestStatus status = ApprovalRequestStatus.New)
        {
            if (status == ApprovalRequestStatus.Approve)
            {
                var requestToApprove = approvalService.ApproveRequest(approvalRequestId);

                if (requestToApprove.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(requestToApprove.Model);
            }
            
            else if(status == ApprovalRequestStatus.Reject)
            {
                var requestToReject = approvalService.RejectRequest(approvalRequestId);

                if (requestToReject.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }

                return Ok(requestToReject.Model);
            }
            else
            {
                return BadRequest();
            }


        }


        }
    }

