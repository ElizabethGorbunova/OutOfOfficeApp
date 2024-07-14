using Microsoft.EntityFrameworkCore;


namespace OutOfOfficeApp.Entities
{
    public class OOODbContext: DbContext
    {
        private string _connectionString = "Server=localhost;Database=OOODbContext;Trusted_Connection=True";
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
                employee.Property(p => p.Id).IsRequired();
                employee.Property(p => p.FullName).IsRequired();
                employee.Property(p => p.Subdivision).IsRequired();
                employee.Property(p => p.Position).IsRequired();
                employee.Property(p => p.Status).IsRequired();
                employee.Property(p => p.OutOfOfficeBalance).IsRequired();
                
            });

            modelBuilder.Entity<LeaveRequest>(request =>
            {
                request.Property(p => p.Id).IsRequired();
                request.Property(p => p.AbsenceReason).IsRequired();
                request.Property(p => p.StartDate).IsRequired();
                request.Property(p => p.EndDate).IsRequired();
                request.Property(p => p.Status).IsRequired();
             
            });

            modelBuilder.Entity<ApprovalRequest>(request =>
            {
                request.Property(p => p.Id).IsRequired();
                request.Property(p => p.Status).IsRequired();
            });

            modelBuilder.Entity<Project>(project =>
            {
                project.Property(p => p.Id).IsRequired();
                project.Property(p => p.ProjectType).IsRequired();
                project.Property(p => p.StartDate).IsRequired();              
                project.Property(p => p.ProjectManager).IsRequired();
                project.Property(p => p.Status).IsRequired();
            });

            modelBuilder.Entity<User>(user =>
            {
                user.Property(p => p.RoleId).IsRequired();
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.ProjectId, ep.EmployeeId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p =>p.EmployeeProjects)
                .HasForeignKey(k => k.ProjectId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(k => k.EmployeeId);

            modelBuilder.Entity<Employee>().HasOne(e => e.PeoplePartner).WithMany().HasForeignKey(e => e.PeoplePartnerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>().HasMany(e => e.ApprovalRequests).WithOne(u=>u.Approver).HasForeignKey(e => e.ApproverId);
            modelBuilder.Entity<Employee>().HasMany(e => e.LeaveRequests).WithOne(u => u.Employee).HasForeignKey(e => e.EmployeeId);
            modelBuilder.Entity<ApprovalRequest>().HasOne(e => e.LeaveRequest).WithOne(u => u.ApprovalRequest).HasForeignKey<LeaveRequest>(e => e.ApprovalRequestId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LeaveRequest>().HasOne(e => e.Employee).WithMany().HasForeignKey(e => e.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LeaveRequest>().HasOne(e => e.ApprovalRequest).WithOne(u => u.LeaveRequest).HasForeignKey<ApprovalRequest>(u => u.LeaveRequestId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasOne(e => e.Employee).WithOne(u => u.User).HasForeignKey<Employee>(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
