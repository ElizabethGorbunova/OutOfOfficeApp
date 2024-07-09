using AutoMapper;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;

namespace OutOfOfficeApp
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDtoOut>();
            CreateMap<EmployeeDtoIn, Employee>();

            CreateMap<ApprovalRequest, ApprovalRequestDtoOut>();

            CreateMap<LeaveRequest, LeaveRequestDtoOut>();
            CreateMap<LeaveRequestDtoIn, LeaveRequest>();

            CreateMap<Project, ProjectDtoOut>();
            CreateMap<ProjectDtoIn, Project>();
        }
    }
}
