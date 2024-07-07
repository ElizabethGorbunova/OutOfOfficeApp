using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IEmployeeService
    {
        public abstract IEnumerable<EmployeeDtoOut> SortEmployees(string columnName);
        public abstract IEnumerable<EmployeeDtoOut> FilterEmployees(string columnName, object value);
        public abstract EmployeeDtoOut AddNewEmployee(Employee employee);
        public abstract EditResult<EmployeeDtoOut> EditEmployee(Employee employee, int ID);
        public abstract EditResult<EmployeeDtoOut> DeactivateEmployee(int ID);
        public abstract EditResult<EmployeeDtoOut> OpenEmployee(int ID);
        public abstract EditResult<EmployeeDtoOut> AssignEmployeeToProject(int employeeID, int projectID);
    }
}
