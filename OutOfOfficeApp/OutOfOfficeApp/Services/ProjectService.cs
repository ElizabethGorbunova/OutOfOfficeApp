using AutoMapper;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace OutOfOfficeApp.Services
{
    public class ProjectService : IProjectService
    {

        private readonly OOODbContext dbContext;
        private readonly IMapper mapper;

        public ProjectService(OOODbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }
        public IEnumerable<ProjectDtoOut> GetAllProjects()
        {
            {
                var allProjects = dbContext.Projects.ToList();
                var projectsDtoOut = mapper.Map<IEnumerable<ProjectDtoOut>>(allProjects);
                return projectsDtoOut;
            }
        }
        public IEnumerable<ProjectDtoOut> SortProjects(IEnumerable<ProjectDtoOut> projects, string columnName)
        {
            string query = $"{columnName}";
            var sortedProjects = projects.AsQueryable().OrderBy(query).ToList();
            var projectsDtoOut = mapper.Map<IEnumerable<ProjectDtoOut>>(sortedProjects);
            return projectsDtoOut;
        }
        public IEnumerable<ProjectDtoOut> FilterProjects(IEnumerable<ProjectDtoOut> projects, string columnName, string value)
        {
            string query = $"{columnName}==@0";

            var column = typeof(Project).GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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

            var filteredProjects = projects.AsQueryable().Where(query, convertedValue).ToList();

            var projectsDtoOut = mapper.Map<IEnumerable<ProjectDtoOut>>(filteredProjects);
            return projectsDtoOut;
        }
        public EditResult<ProjectDtoOut> OpenProject(int ID)
        {
            var projectToOpen = dbContext.Projects.FirstOrDefault(e => e.ProjectId == ID);

            if (projectToOpen == null)
            {
                return new EditResult<ProjectDtoOut>() { IsSuccess = false, Model = null };
            }

            var projectDtoOut = mapper.Map<ProjectDtoOut>(projectToOpen);

            return new EditResult<ProjectDtoOut>() { IsSuccess = true, Model = projectDtoOut };
        }
        public ProjectDtoOut AddNewProject(ProjectDtoIn project)
        {
            var projectMapped = mapper.Map<Project>(project);
            projectMapped.Status = Enums.Status.Active;
            dbContext.Projects.Add(projectMapped);
            dbContext.SaveChanges();

            var projectDtoOut = mapper.Map<ProjectDtoOut>(projectMapped);
            return projectDtoOut;
        }
        public EditResult<ProjectDtoOut> EditProject(ProjectDtoIn project, int ID)
        {
            var projectToEdit = dbContext.Projects.FirstOrDefault(e => e.ProjectId == ID);

            if (projectToEdit == null)
            {
                return new EditResult<ProjectDtoOut>() { IsSuccess = false, Model = null };
            }

            projectToEdit.ProjectType = project.ProjectType;
            projectToEdit.StartDate = project.StartDate;
            projectToEdit.EndDate = project.EndDate;
            projectToEdit.ProjectManager = project.ProjectManager;
            projectToEdit.Comment = project.Comment;
            dbContext.SaveChanges();

            var projectDtoOut = mapper.Map<ProjectDtoOut>(projectToEdit);

            return new EditResult<ProjectDtoOut>() { IsSuccess = true, Model = projectDtoOut };
        }
        public EditResult<ProjectDtoOut> DeactivateProject(int ID)
        {
            var projectToDeactivate = dbContext.Projects.FirstOrDefault(e => e.ProjectId == ID);

            if (projectToDeactivate == null)
            {
                return new EditResult<ProjectDtoOut>() { IsSuccess = false, Model = null };
            }

            projectToDeactivate.Status = Enums.Status.Inactive;
            dbContext.SaveChanges();

            var projectDtoOut = mapper.Map<ProjectDtoOut>(projectToDeactivate);

            return new EditResult<ProjectDtoOut>() { IsSuccess = true, Model = projectDtoOut };
        }

        
        
    }
}
