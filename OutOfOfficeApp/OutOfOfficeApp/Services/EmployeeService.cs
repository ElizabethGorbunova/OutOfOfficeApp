using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using AutoMapper;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

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
        public IEnumerable<EmployeeDtoOut> GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            var employeesDtoOut = mapper.Map<IEnumerable<EmployeeDtoOut>>(allEmployees);
            return employeesDtoOut;
        }
        public IEnumerable<EmployeeDtoOut> SortEmployees(IEnumerable<EmployeeDtoOut> employees, string columnName)
        {
            string query = $"{columnName}";
            var sortedEmployees = employees.AsQueryable().OrderBy(query).ToList();
            var employeesDtoOut = mapper.Map<IEnumerable<EmployeeDtoOut>>(sortedEmployees);
            return employeesDtoOut;
        }
        public IEnumerable<EmployeeDtoOut> FilterEmployees(IEnumerable<EmployeeDtoOut> employees, string columnName, string value)
        {
            string query = $"{columnName}==@0";

            var column = typeof(Employee).GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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

            var filteredEmployees = employees.AsQueryable().Where(query, convertedValue).ToList();

            var employeesDtoOut = mapper.Map<IEnumerable<EmployeeDtoOut>>(filteredEmployees);
            return employeesDtoOut;
        }
        public EditResult<EmployeeDtoOut> OpenEmployee(int ID)
        {
            var employeeToOpen = dbContext.Employees.FirstOrDefault(e => e.Id == ID);

            if (employeeToOpen == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeToOpen);

            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut };
        }
        public EmployeeDtoOut AddNewEmployee(EmployeeDtoIn employee)
        {
            var employeeMapped = mapper.Map<Employee>(employee);
            dbContext.Employees.Add(employeeMapped);
            dbContext.SaveChanges();

            var user = dbContext.Users.FirstOrDefault(u => u.Id == employeeMapped.UserId);
            user.EmployeeId = employeeMapped.Id;
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeMapped);
            return employeeDtoOut;
        }
        public EditResult<EmployeeDtoOut> EditEmployee(EmployeeDtoIn employee, int ID)
        {
            var employeeToEdit = dbContext.Employees.FirstOrDefault(e => e.Id == ID);

            if (employeeToEdit == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            employeeToEdit.FullName = employee.FullName;
            employeeToEdit.Subdivision = employee.Subdivision;
            employeeToEdit.Position = employee.Position;
            employeeToEdit.Status = employee.Status;
            employeeToEdit.PeoplePartnerId = employee.PeoplePartnerId;
            employeeToEdit.OutOfOfficeBalance = employee.OutOfOfficeBalance;
            employeeToEdit.Photo = employee.Photo;
            employeeToEdit.UserId = employee.UserId;
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeToEdit);
            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut };
        }
        public EditResult<EmployeeDtoOut> AssignEmployeeToProject(int employeeID, int projectID)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.Id == employeeID);

            if (employee == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            var project = dbContext.Projects.FirstOrDefault(e => e.Id == projectID);

            if (project == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            employee.Projects.Add(project);
            

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employee);

            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut };
        }
        public EditResult<EmployeeDtoOut> DeactivateEmployee(int ID)
        {
            var employeeToDeactivate = dbContext.Employees.FirstOrDefault(e => e.Id == ID);

            if (employeeToDeactivate == null)
            {
                return new EditResult<EmployeeDtoOut>() { IsSuccess = false, Model = null };
            }

            employeeToDeactivate.Status = Enums.Status.Inactive;
            dbContext.SaveChanges();

            var employeeDtoOut = mapper.Map<EmployeeDtoOut>(employeeToDeactivate);

            return new EditResult<EmployeeDtoOut>() { IsSuccess = true, Model = employeeDtoOut };
        }

       
        
    }
}
