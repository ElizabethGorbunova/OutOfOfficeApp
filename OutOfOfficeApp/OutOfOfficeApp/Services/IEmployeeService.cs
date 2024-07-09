using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDtoOut> GetAllEmployees();
        public abstract IEnumerable<EmployeeDtoOut> SortEmployees(IEnumerable<EmployeeDtoOut> employees, string columnName);
        public abstract IEnumerable<EmployeeDtoOut> FilterEmployees(IEnumerable<EmployeeDtoOut> employees, string columnName, string value);
        public abstract EmployeeDtoOut AddNewEmployee(EmployeeDtoIn employee);
        public abstract EditResult<EmployeeDtoOut> EditEmployee(EmployeeDtoIn employee, int ID);
        public abstract EditResult<EmployeeDtoOut> DeactivateEmployee(int ID);
        public abstract EditResult<EmployeeDtoOut> OpenEmployee(int ID);
        public abstract EditResult<EmployeeDtoOut> AssignEmployeeToProject(int employeeID, int projectID);
    }
}
