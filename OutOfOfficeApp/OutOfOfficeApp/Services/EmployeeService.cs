using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using AutoMapper;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace OutOfOfficeApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly OOODbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeService(OOODbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }
        public IEnumerable<EmployeeDtoOut> SortEmployees(string columnName)
        {
            string query = $"{columnName} == @0";
            var sortedEmployees = dbContext.Employees.OrderBy(query).ToList();
            var employeesDtoOut = mapper.Map<IEnumerable<EmployeeDtoOut>>(sortedEmployees);
            return employeesDtoOut;
        }

        public IEnumerable<EmployeeDtoOut> FilterEmployees(string columnName, object value)
        {
            string query = $"{columnName} == @0";
            var filteredEmployees = dbContext.Employees.Where(query, value).ToList();
            var employeesDtoOut = mapper.Map<IEnumerable<EmployeeDtoOut>>(filteredEmployees);
            return employeesDtoOut;
        }

        public EmployeeDtoOut AddNewEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employee);
            return employeeDtoOut;
        }

        public EditResult<EmployeeDtoOut> EditEmployee(Employee employee, int ID)
        {
            var employeeToEdit = dbContext.Employees.FirstOrDefault(e => e.EmployeeID == ID);

            if(employeeToEdit == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            employeeToEdit.FullName = employee.FullName;
            employeeToEdit.Subdivision = employee.Subdivision;
            employeeToEdit.Position = employee.Position;
            employeeToEdit.Status = employee.Status;
            employeeToEdit.PeoplePartner = employee.PeoplePartner;
            employeeToEdit.OutOfOfficeBalance = employee.OutOfOfficeBalance;
            employeeToEdit.Photo = employee.Photo;
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeToEdit);
            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut } ;
        }

        public EditResult<EmployeeDtoOut> DeactivateEmployee (int ID)
        {
            var employeeToDeactivate = dbContext.Employees.FirstOrDefault(e => e.EmployeeID == ID);

            if (employeeToDeactivate == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            employeeToDeactivate.Status = Enums.Status.Inactive;
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeToDeactivate);

            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut};
        }

        public EditResult<EmployeeDtoOut> OpenEmployee(int ID)
        {
            var employeeToOpen = dbContext.Employees.FirstOrDefault(e => e.EmployeeID == ID);

            if (employeeToOpen == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeToOpen);

            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut };
        }

        public EditResult<EmployeeDtoOut> AssignEmployeeToProject(int employeeID, int projectID)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeID == employeeID);

            if (employee == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            var project = dbContext.Projects.FirstOrDefault(e => e.ProjectID == projectID);

            if (project == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            employee.Projects.Add(project);
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employee);

            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut };
        }


    }
}
