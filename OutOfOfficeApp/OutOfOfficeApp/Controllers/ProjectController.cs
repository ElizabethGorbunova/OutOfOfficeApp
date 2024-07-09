using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Enums;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/projects")]
    /*[Authorize]*/
    public class ProjectController:ControllerBase
    {
        public readonly OOODbContext dbContext;
        public readonly IProjectService projectService;
        private readonly IMapper mapper;
        public ProjectController(OOODbContext _dbContext, IProjectService _projectService, IMapper _mapper)
        {
            dbContext = _dbContext;
            projectService = _projectService;
            mapper = _mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<ProjectDtoOut>> GetSortedOrFilteredProjects([FromQuery(Name = "sortBy")] string? columnNameToSortBy = null, [FromQuery(Name = "filter")] Dictionary<string, string>? filterParams = null)
        {

            IEnumerable<ProjectDtoOut> sortedFilteredProjects = projectService.GetAllProjects();

            if (!string.IsNullOrEmpty(columnNameToSortBy))
            {
                sortedFilteredProjects = projectService.SortProjects(sortedFilteredProjects, columnNameToSortBy);
                if (sortedFilteredProjects == null)
                {
                    return NotFound("No projects found with the given criteria.");
                }
            }

            if (filterParams != null)
            {
                foreach (var pair in filterParams)
                {
                    sortedFilteredProjects = projectService.FilterProjects(sortedFilteredProjects, filterParams["columnName"], filterParams["value"]);
                    if (sortedFilteredProjects == null)
                    {
                        return NotFound("No projects found with the given criteria.");
                    }
                }

            }

            return Ok(sortedFilteredProjects);



        }
        
        [HttpGet("{id}")]
        public ActionResult<ProjectDtoOut> OpenProject(int id)
        {
            var openedProject = projectService.OpenProject(id);
            if (openedProject.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(openedProject.Model);

        }

        [HttpPost]
        public ActionResult<EmployeeDtoOut> AddNewProject([FromBody] ProjectDtoIn project)
        {
            var newProject = projectService.AddNewProject(project);

            return Ok(newProject);
        }

        [HttpPut("{id}")]
        public ActionResult<ProjectDtoOut> UpdateProject(int id, [FromBody] ProjectDtoIn? project = null, [FromQuery(Name = "status")] Status status = Status.Active)
        {
            if (status == Status.Inactive)
            {
                var projectToDeactivate = projectService.DeactivateProject(id);

                if (projectToDeactivate.IsSuccess == false)
                {
                    return NotFound("Not found:(");
                }
                return Ok(projectToDeactivate.Model);
            }

            var updatedProject = projectService.EditProject(project, id);

            if (updatedProject.IsSuccess == false)
            {
                return NotFound("Not found:(");
            }

            return Ok(updatedProject.Model);
            

        }

    }
}
