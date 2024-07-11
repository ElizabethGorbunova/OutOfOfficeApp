using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Enums;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;
using System.Security.Claims;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/leave-requests")]
    [Authorize]
    public class LeaveRequestController:ControllerBase
    {
        public readonly OOODbContext dbContext;
        public readonly ILeaveRequestService leaveService;
        private readonly IMapper mapper;
        public LeaveRequestController(OOODbContext _dbContext, ILeaveRequestService _leaveService, IMapper _mapper)
        {
            dbContext = _dbContext;
            leaveService = _leaveService;
            mapper = _mapper;
        }

        [Authorize(Roles = "HRManager, ProjectManager, Employee, Administrator")]
        [HttpGet()]
        public ActionResult<IEnumerable<LeaveRequestDtoOut>> GetSortedOrFilteredLeaveRequests([FromQuery(Name = "sortBy")] string? columnNameToSortBy = null, [FromQuery(Name = "filter")] Dictionary<string, string>? filterParams = null)
        {
            IEnumerable<LeaveRequestDtoOut> sortedFilteredRequests;
            var employeeRole = HttpContext.User.IsInRole("Employee");
            if (employeeRole)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                sortedFilteredRequests = leaveService.GetAllLeaveRequestsForCurrentEmployee(userId);
            }
            else {
                
                sortedFilteredRequests = leaveService.GetAllLeaveRequests();
            };

            if (!string.IsNullOrEmpty(columnNameToSortBy))
            {
                sortedFilteredRequests = leaveService.SortLeaveRequests(sortedFilteredRequests, columnNameToSortBy);
                if (sortedFilteredRequests == null)
                {
                    return NotFound("No requests found with the given criteria.");
                }
            }

            if (filterParams != null)
            {
                foreach (var pair in filterParams)
                {
                    sortedFilteredRequests = leaveService.FilterLeaveRequests(sortedFilteredRequests, filterParams["columnName"], filterParams["value"]);
                    if (sortedFilteredRequests == null)
                    {
                        return NotFound("No requests found with the given criteria.");
                    }
                }

            }

            return Ok(sortedFilteredRequests);

        }

        [Authorize(Roles = "HRManager, ProjectManager, Employee, Administrator")]
        [HttpGet("{leaveRequestId}")]
        public ActionResult<LeaveRequestDtoOut> OpenLeaveRequest(int leaveRequestId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var openedRequest = leaveService.OpenLeaveRequest(leaveRequestId, userId);
            if (openedRequest.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(openedRequest.Model);

        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPost]
        public ActionResult<LeaveRequestDtoOut> AddNewLeaveRequest([FromBody] LeaveRequestDtoIn newRequest)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createdRequest = leaveService.CreateLeaveRequest(newRequest, userId);

            return Ok(createdRequest.Model);
        }

        [Authorize(Roles = "Employee, Administrator")]
        [HttpPut("{leaveRequestId}")]
        public ActionResult<LeaveRequestDtoOut> UpdateLeaveRequest(int leaveRequestId, [FromQuery(Name = "status")] LeaveRequestStatus status = LeaveRequestStatus.New, [FromBody] LeaveRequestDtoIn? leaveRequest = null)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           

            if (status == LeaveRequestStatus.Submit)
            {
                var requestToSubmit = leaveService.SubmitLeaveRequest(leaveRequestId, userId);

                if (requestToSubmit.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(requestToSubmit.Model);
            }
            if(status == LeaveRequestStatus.Cancel)
            {
                var requestToCancel = leaveService.CancelLeaveRequest(leaveRequestId, userId);

                if (requestToCancel.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(requestToCancel.Model);
            }
            
            var updatedEmployee = leaveService.EditLeaveRequest(leaveRequest, leaveRequestId, userId);

            if (updatedEmployee.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(updatedEmployee.Model);

            


        }


    }
}
