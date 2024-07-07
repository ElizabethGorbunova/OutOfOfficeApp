using AutoMapper;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.Linq.Dynamic.Core;

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
        public IEnumerable<ProjectDtoOut> SortProjects(string columnName)
        {
            string query = $"{columnName} == @0";
            var sortedProjects = dbContext.Projects.OrderBy(query).ToList();
            var projectsDtoOut = mapper.Map<IEnumerable<ProjectDtoOut>>(sortedProjects);
            return projectsDtoOut;
        }
        public IEnumerable<ProjectDtoOut> FilterProjects(string columnName, object value)
        {
            string query = $"{columnName} == @0";
            var filteredProjects = dbContext.Projects.Where(query, value).ToList();
            var projectsDtoOut = mapper.Map<IEnumerable<ProjectDtoOut>>(filteredProjects);
            return projectsDtoOut;
        }
        public EditResult<ProjectDtoOut> OpenProject(int ID)
        {
            var projectToOpen = dbContext.Projects.FirstOrDefault(e => e.ProjectID == ID);

            if (projectToOpen == null)
            {
                return new EditResult<ProjectDtoOut>() { IsSuccess = false, Model = null };
            }

            var projectDtoOut = mapper.Map<ProjectDtoOut>(projectToOpen);

            return new EditResult<ProjectDtoOut>() { IsSuccess = true, Model = projectDtoOut };
        }
        public ProjectDtoOut AddNewProject(Project project)
        {
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();

            var projectDtoOut = mapper.Map<ProjectDtoOut>(project);
            return projectDtoOut;
        }
        public EditResult<ProjectDtoOut> EditProject(Project project, int ID)
        {
            var projectToEdit = dbContext.Projects.FirstOrDefault(e => e.ProjectID == ID);

            if (projectToEdit == null)
            {
                return new EditResult<ProjectDtoOut>() { IsSuccess = false, Model = null };
            }

            projectToEdit.ProjectType = project.ProjectType;
            projectToEdit.StartDate = project.StartDate;
            projectToEdit.EndDate = project.EndDate;
            projectToEdit.Status = project.Status;
            projectToEdit.ProjectManager = project.ProjectManager;
            projectToEdit.Comment = project.Comment;
            projectToEdit.Status = project.Status;
            dbContext.SaveChanges();

            var projectDtoOut = mapper.Map<ProjectDtoOut>(projectToEdit);

            return new EditResult<ProjectDtoOut>() { IsSuccess = true, Model = projectDtoOut };
        }
        public EditResult<ProjectDtoOut> DeactivateProject(int ID)
        {
            var projectToDeactivate = dbContext.Projects.FirstOrDefault(e => e.ProjectID == ID);

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
