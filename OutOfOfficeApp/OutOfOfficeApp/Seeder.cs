using Microsoft.IdentityModel.Tokens;
using OutOfOfficeApp.Entities;

namespace OutOfOfficeApp
{
    public class Seeder
    {
        private readonly OOODbContext dbContext;
        public Seeder(OOODbContext _DbContext)
        {
            dbContext = _DbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Employees.Any())
                {
                    dbContext.Employees.AddRange(GetEmployees());
                    dbContext.SaveChanges();
                }

                if (!dbContext.ApprovalRequests.Any())
                {
                    dbContext.ApprovalRequests.AddRange(GetApprovalRequests());
                    dbContext.SaveChanges();
                }

                if (!dbContext.LeaveRequests.Any())
                {
                    dbContext.LeaveRequests.AddRange(GetLeaveRequests());
                    dbContext.SaveChanges();
                }

                if (!dbContext.Projects.Any())
                {
                    dbContext.Projects.AddRange(GetProjects());
                    dbContext.SaveChanges();
                }
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(GetRoles());
                    dbContext.SaveChanges();
                }
            }
        }

        private List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    /*EmployeeID = 1,*/
                    FullName= "Person1",
                    OutOfOfficeBalance= 23,
                    PeoplePartner = 2,
                    Position = Enums.Position.BackendDeveloper,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.ITDepartment
                },

                new Employee()
                {
                    /*EmployeeID = 2,*/
                    FullName= "Person2",
                    OutOfOfficeBalance= 27,
                    PeoplePartner = 1,
                    Position = Enums.Position.ProjectManager,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.ITDepartment
                },

                new Employee()
                {
                    /*EmployeeID = 3,*/
                    FullName= "Person3",
                    OutOfOfficeBalance= 37,
                    PeoplePartner = 2,
                    Position = Enums.Position.HRManager,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.HRDepartment
                }

            };

            return employees;   
        }

        private List<LeaveRequest> GetLeaveRequests()
        {
            List<LeaveRequest> leaveRequests = new List<LeaveRequest>()
            {
                new LeaveRequest()
                {
                    /*LeaveRequestID = 1,*/
                    Employee = 1,
                    AbsenceReason = Enums.AbsenceReason.Health_Issues,
                    StartDate = new DateTime(2024, 07, 18),
                    EndDate = new DateTime(2024, 07, 25),
                    Comment = "Person2 will be my replacement",
                    Status = Enums.LeaveRequestStatus.New
                },

                new LeaveRequest()
                {
                    /*LeaveRequestID = 2,*/
                    Employee = 2,
                    AbsenceReason = Enums.AbsenceReason.Vacation_Leave,
                    StartDate = new DateTime(2024, 08, 01),
                    EndDate = new DateTime(2024, 08, 08),
                    Comment = "Person1 will be my replacement",
                    Status = Enums.LeaveRequestStatus.New
                },

                new LeaveRequest()
                {
                    /*LeaveRequestID = 3,*/
                    Employee = 3,
                    AbsenceReason = Enums.AbsenceReason.ParentalDuties,
                    StartDate = new DateTime(2024, 07, 22),
                    EndDate = new DateTime(2024, 07, 22),
                    Comment = "I'm gonna be OOO on 22.07.",
                    Status = Enums.LeaveRequestStatus.New
                }

            };

            return leaveRequests;
        }

        private List<ApprovalRequest> GetApprovalRequests()
        {
            List<ApprovalRequest> approvalRequests = new List<ApprovalRequest>()
            {
                new ApprovalRequest()
                {
                    /*ApprovalRequestID = 1,*/
                    Approver = 2,
                    LeaveRequest = 1,
                    Comment = "The request is approved"
                   
                },

                new ApprovalRequest()
                {
                    /*ApprovalRequestID = 2,*/
                    Approver = 3,
                    LeaveRequest = 2,
                    Comment = "The request is approved"
                },

                new ApprovalRequest()
                {
                    /*ApprovalRequestID = 3,*/
                    Approver = 3,
                    LeaveRequest = 3,
                    Comment = "The request is approved"
                }

            };

            return approvalRequests;
        }

        private List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>()
            {
                new Project()
                {
                    /*ProjectID = 1,*/
                    ProjectManager = 2,
                    ProjectType = Enums.ProjectType.OutOfOfficeApplication,
                    StartDate = new DateTime(2024, 01, 15),
                    Comment = "In progress",
                    Status = Enums.Status.Active
                },

                new Project()
                {
                    /*ProjectID = 2,*/
                    ProjectManager = 2,
                    ProjectType = Enums.ProjectType.OCRInvoiceDataStruktureProject,
                    StartDate = new DateTime(2023, 05, 02),
                    EndDate = new DateTime(2023, 11, 19),
                    Comment = "Relized in production",
                    Status = Enums.Status.Inactive
                },

            };

            return projects;
        }

        private List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>()
            {
                new Role()
                {
                    Name= "HRManager"
                },

                new Role()
                {
                    Name = "ProjectManager"
                },
                new Role()
                {
                    Name = "Employee"
                },
                new Role()
                {
                    Name = "Administrator"
                },

            };

            return roles;
        }
    }
}
