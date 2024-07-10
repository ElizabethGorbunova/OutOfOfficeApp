using Microsoft.EntityFrameworkCore;


namespace OutOfOfficeApp.Entities
{
    public class OOODbContext: DbContext
    {
        private string _connectionString = "rServer=localhost;Database=OOODbContext;Trusted_Connection=True";
        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<LeaveRequest> LeaveRequests { get; set; }
        internal DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        internal DbSet<Project> Projects { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(employee =>
            {
                employee.Property(p => p.EmployeeID).IsRequired();
                employee.Property(p => p.FullName).IsRequired();
                employee.Property(p => p.Subdivision).IsRequired();
                employee.Property(p => p.Position).IsRequired();
                employee.Property(p => p.Status).IsRequired();
                employee.Property(p => p.PeoplePartner).IsRequired();
                employee.Property(p => p.OutOfOfficeBalance).IsRequired();
            });

            modelBuilder.Entity<LeaveRequest>(request =>
            {
                request.Property(p => p.LeaveRequestID).IsRequired();
                request.Property(p => p.Employee).IsRequired();
                request.Property(p => p.AbsenceReason).IsRequired();
                request.Property(p => p.StartDate).IsRequired();
                request.Property(p => p.EndDate).IsRequired();
                request.Property(p => p.Status).IsRequired();
            });

            modelBuilder.Entity<ApprovalRequest>(request =>
            {
                request.Property(p => p.ApprovalRequestID).IsRequired();
                request.Property(p => p.Approver).IsRequired();
                request.Property(p => p.LeaveRequest).IsRequired();
                request.Property(p => p.Status).IsRequired();
            });

            modelBuilder.Entity<Project>(project =>
            {
                project.Property(p => p.ProjectID).IsRequired();
                project.Property(p => p.ProjectType).IsRequired();
                project.Property(p => p.StartDate).IsRequired();              
                project.Property(p => p.ProjectManager).IsRequired();
                project.Property(p => p.Status).IsRequired();
            });

            modelBuilder.Entity<User>(user =>
            {
                user.Property(p => p.RoleId).IsRequired();
                user.Property(p => p.EmployeeId).IsRequired();
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.Property(p => p.Name).IsRequired();
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
