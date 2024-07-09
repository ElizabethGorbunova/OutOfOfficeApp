using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Enums;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/leaveRequests")]
    /*[Authorize]*/
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

        [HttpGet()]
        public ActionResult<IEnumerable<LeaveRequestDtoOut>> GetSortedOrFilteredLeaveRequests([FromQuery(Name = "sortBy")] string? columnNameToSortBy = null, [FromQuery(Name = "filter")] Dictionary<string, string>? filterParams = null)
        {
            IEnumerable<LeaveRequestDtoOut> sortedFilteredRequests = leaveService.GetAllLeaveRequests();

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

        [HttpGet("{id}")]
        public ActionResult<LeaveRequestDtoOut> OpenLeaveRequest(int id)
        {
            var openedRequest = leaveService.OpenLeaveRequest(id);
            if (openedRequest.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(openedRequest.Model);

        }

        [HttpPost]
        public ActionResult<LeaveRequestDtoOut> AddNewLeaveRequest([FromBody] LeaveRequestDtoIn newRequest)
        {
            var createdRequest = leaveService.CreateLeaveRequest(newRequest);

            return Ok(createdRequest.Model);
        }

        [HttpPut("{id}")]
        public ActionResult<LeaveRequestDtoOut> UpdateLeaveRequest(int id, [FromQuery(Name = "status")] LeaveRequestStatus status = LeaveRequestStatus.New, [FromBody] LeaveRequestDtoIn? leaveRequest = null)
        {
            if (status == LeaveRequestStatus.Submit)
            {
                var requestToSubmit = leaveService.SubmitLeaveRequest(id);

                if (requestToSubmit.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(requestToSubmit.Model);
            }
            if(status == LeaveRequestStatus.Cancel)
            {
                var requestToCancel = leaveService.CancelLeaveRequest(id);

                if (requestToCancel.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(requestToCancel.Model);
            }
            
            var updatedEmployee = leaveService.EditLeaveRequest(leaveRequest, id);

            if (updatedEmployee.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(updatedEmployee.Model);

            


        }


    }
}
