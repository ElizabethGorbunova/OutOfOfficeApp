﻿using Microsoft.IdentityModel.Tokens;
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
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(GetRoles());
                    dbContext.SaveChanges();
                }

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.AddRange(GetUsers());
                    dbContext.SaveChanges();
                }
                if (!dbContext.Employees.Any())
                {
                    dbContext.Employees.AddRange(GetEmployees());
                    dbContext.SaveChanges();
                }
                if (!dbContext.LeaveRequests.Any())
                {
                    dbContext.LeaveRequests.AddRange(GetLeaveRequests());
                    dbContext.SaveChanges();
                }


                if (!dbContext.ApprovalRequests.Any())
                {
                    dbContext.ApprovalRequests.AddRange(GetApprovalRequests());
                    dbContext.SaveChanges();
                }

                
                if (!dbContext.Projects.Any())
                {
                    dbContext.Projects.AddRange(GetProjects());
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
                    FullName= "Person1",
                    OutOfOfficeBalance= 23,
                    Position = Enums.Position.BackendDeveloper,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.ITDepartment,
                },

                new Employee()
                {
                    FullName= "Person2",
                    OutOfOfficeBalance= 27,
                    Position = Enums.Position.ProjectManager,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.ITDepartment,

                },

                new Employee()
                {
                    FullName= "Person3",
                    OutOfOfficeBalance= 37,
                    Position = Enums.Position.HRManager,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.HRDepartment,
                },

                new Employee()
                {
                    FullName= "Person4",
                    OutOfOfficeBalance= 40,
                    Position = Enums.Position.Administrator,
                    Status = Enums.Status.Active,
                    Subdivision= Enums.Subdivision.ITDepartment,
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
                    EmployeeId = null,
                    AbsenceReason = Enums.AbsenceReason.Health_Issues,
                    StartDate = new DateTime(2024, 07, 18),
                    EndDate = new DateTime(2024, 07, 25),
                    Comment = "Person2 will be my replacement",
                    Status = Enums.LeaveRequestStatus.New
                },

                new LeaveRequest()
                {
                    EmployeeId = null,
                    AbsenceReason = Enums.AbsenceReason.Vacation_Leave,
                    StartDate = new DateTime(2024, 08, 01),
                    EndDate = new DateTime(2024, 08, 08),
                    Comment = "Person1 will be my replacement",
                    Status = Enums.LeaveRequestStatus.New
                },

                new LeaveRequest()
                {
                    EmployeeId = null,
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
                    ApproverId = null,
                    LeaveRequestId = null,
                    Comment = "The request is in progress"
                   
                },

                new ApprovalRequest()
                {
                    ApproverId = null,
                    LeaveRequestId = null,
                    Comment = "The request is in progress"
                },

                new ApprovalRequest()
                {
                    ApproverId = null,
                    LeaveRequestId = null,
                    Comment = "The request is in progress"
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
                    ProjectManager = null,
                    ProjectType = Enums.ProjectType.OutOfOfficeApplication,
                    StartDate = new DateTime(2024, 01, 15),
                    Comment = "In progress",
                    Status = Enums.Status.Active
                },

                new Project()
                {
                    ProjectManager = null,
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

        private List<User> GetUsers()
        {
            List<User> users = new List<User>()
            {
                new User()
                {
                    FirstName= "Person1",
                    RoleId= null
                },

                new User()
                {
                    FirstName= "Person2",
                    RoleId= null
                },
                new User()
                {
                    FirstName = "Person3",
                    RoleId =null
                },
                new User()
                {
                    FirstName = "Person4",
                    RoleId = null
                },

            };

            return users;
        }
    }
}
