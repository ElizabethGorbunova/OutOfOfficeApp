using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IProjectService
    {
        public abstract IEnumerable<ProjectDtoOut> SortProjects(string columnName);
        public abstract IEnumerable<ProjectDtoOut> FilterProjects(string columnName, object value);
        public abstract EditResult<ProjectDtoOut> OpenProject(int ID);
        public abstract ProjectDtoOut AddNewProject(Project project);
        public abstract EditResult<ProjectDtoOut> EditProject(Project project, int ID);
        public abstract EditResult<ProjectDtoOut> DeactivateProject(int ID);

    }
}
