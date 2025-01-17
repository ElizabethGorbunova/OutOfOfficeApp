﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Enums;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;
using System.Security.Claims;
using System.Xml.Linq;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public class EmployeeController: ControllerBase
    {
        public readonly OOODbContext dbContext;
        public readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        public EmployeeController(OOODbContext _dbContext, IEmployeeService _employeeService, IMapper _mapper)
        {
            dbContext = _dbContext;
            employeeService = _employeeService;
            mapper = _mapper;
        }


        [Authorize(Roles = "HRManager, ProjectManager, Administrator")]
        [HttpGet()]
        public ActionResult<IEnumerable<EmployeeDtoOut>> GetSortedOrFilteredEmployees([FromQuery(Name = "sortBy")] string? columnNameToSortBy=null, [FromQuery(Name = "filter")] Dictionary<string, string>? filterParams = null)
        {

            IEnumerable<EmployeeDtoOut> sortedFilteredEmployees = employeeService.GetAllEmployees();

            if (!string.IsNullOrEmpty(columnNameToSortBy))
            {
                sortedFilteredEmployees = employeeService.SortEmployees(sortedFilteredEmployees, columnNameToSortBy);
                if (sortedFilteredEmployees == null)
                {
                    return NotFound("No employees found with the given criteria.");
                }
            }

            if (filterParams != null)
            {
                foreach(var pair in filterParams)
                {
                    sortedFilteredEmployees = employeeService.FilterEmployees(sortedFilteredEmployees, filterParams["columnName"], filterParams["value"]);
                    if (sortedFilteredEmployees == null)
                    {
                        return NotFound("No employees found with the given criteria.");
                    }
                }
                
            }

            return Ok(sortedFilteredEmployees);



        }

        [Authorize(Roles = "ProjectManager, Administrator")]
        [HttpGet("{employeeId}")]
        public ActionResult<EmployeeDtoOut> OpenEmployee(int employeeId)
        {
            var openedEmployee = employeeService.OpenEmployee(employeeId);
            if(openedEmployee.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(openedEmployee.Model);

        }

        [Authorize(Roles = "HRManager, Administrator")]
        [HttpPost]
        public ActionResult<EmployeeDtoOut> AddNewEmployee([FromBody] EmployeeDtoIn employee)
        {
            var newEmployee = employeeService.AddNewEmployee(employee);
            
            return Ok(newEmployee);
        }


        [Authorize(Roles = "HRManager, ProjectManager, Administrator")]
        [HttpPut("{employeeId}")]
        public ActionResult<EmployeeDtoOut> UpdateEmployee(int employeeId, [FromBody] EmployeeDtoIn? employee = null, [FromQuery(Name = "status")] Status status = Status.Active, [FromQuery(Name = "projectId")] int projectId=0)
        {

            if (status == Status.Inactive)
            {
                var localAuthorizationDeactivation = HttpContext.User.IsInRole("HRManager") || HttpContext.User.IsInRole("Administrator");
                
                if (!localAuthorizationDeactivation)
                {
                    return Forbid();
                }
                var employeeToDeactivate = employeeService.DeactivateEmployee(employeeId);

                if (employeeToDeactivate.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(employeeToDeactivate.Model);
            }

            if(projectId != 0)
            {
                var localAuthorizationAssigning = HttpContext.User.IsInRole("ProjectManager") || HttpContext.User.IsInRole("Administrator");
                if (!localAuthorizationAssigning)
                {
                    return Forbid();
                }
                var employeeWithAssignedProject = employeeService.AssignEmployeeToProject(employeeId, projectId);

                if (employeeWithAssignedProject.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }

                return Ok(employeeWithAssignedProject.Model);
            }

            else
            {
                var localAuthorizationEditing = HttpContext.User.IsInRole("HRManager") || HttpContext.User.IsInRole("Administrator");
                
                if (!localAuthorizationEditing)
                {
                    return Forbid();
                }
                var updatedEmployee = employeeService.EditEmployee(employee, employeeId);

                if (updatedEmployee.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }

                return Ok(updatedEmployee.Model);
            }

            

            
                
        }

       

    }
}
