using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Enums;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/approvalRequests")]
    /*[Authorize]*/
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

        [HttpGet("{id}")]
        public ActionResult<ApprovalRequestDtoOut> OpenApprovalRequest(int id)
        {
            var openedRequest = approvalService.OpenApprovalRequest(id);
            if (openedRequest.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(openedRequest.Model);

        }

        [HttpPut("{id}")]
        public ActionResult<ApprovalRequestDtoOut> UpdateApprovalRequest(int id, [FromQuery(Name = "status")] ApprovalRequestStatus status = ApprovalRequestStatus.New)
        {
            if (status == ApprovalRequestStatus.Approve)
            {
                var requestToApprove = approvalService.ApproveRequest(id);

                if (requestToApprove.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(requestToApprove.Model);
            }
            
            else if(status == ApprovalRequestStatus.Reject)
            {
                var requestToReject = approvalService.RejectRequest(id);

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

