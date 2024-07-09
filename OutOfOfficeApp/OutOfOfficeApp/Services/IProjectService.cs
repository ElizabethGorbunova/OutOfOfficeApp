using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IProjectService
    {
        public IEnumerable<ProjectDtoOut> GetAllProjects();
        public abstract IEnumerable<ProjectDtoOut> SortProjects(IEnumerable<ProjectDtoOut> projects, string columnName);
        public abstract IEnumerable<ProjectDtoOut> FilterProjects(IEnumerable<ProjectDtoOut> projects, string columnName, string value);
        public abstract EditResult<ProjectDtoOut> OpenProject(int ID);
        public abstract ProjectDtoOut AddNewProject(ProjectDtoIn project);
        public abstract EditResult<ProjectDtoOut> EditProject(ProjectDtoIn project, int ID);
        public abstract EditResult<ProjectDtoOut> DeactivateProject(int ID);

    }
}
