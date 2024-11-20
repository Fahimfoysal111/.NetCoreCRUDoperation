using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeASPNET.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> opt) : base(opt)
        {
        }

        public DbSet<Project>? Projects { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<ProjectAssignment>? ProjectAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    EmployeeName = "John Doe",
                    Age = 30,
                    DateOfJoining = new DateTime(2020, 5, 1),
                    IsActive = true,
                    image = "john.jpg"
                },
                new Employee
                {
                    EmployeeId = 2,
                    EmployeeName = "Jane Smith",
                    Age = 25,
                    DateOfJoining = new DateTime(2022, 8, 15),
                    IsActive = true,
                    image = "jane.jpg"
                }
            );

            // Seed data for Projects
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    ProjectId = 1,
                    ProjectName = "HTML"
                },
                new Project
                {
                    ProjectId = 2,
                    ProjectName = "CSS"
                },
                 new Project
                 {
                     ProjectId = 3,
                     ProjectName = "SQL"
                 }
            );

            // Seed data for ProjectAssignments
            modelBuilder.Entity<ProjectAssignment>().HasData(
                new ProjectAssignment
                {
                    AssignmentId = 1,
                    EmployeeId = 1,
                    ProjectId = 1
                },
                new ProjectAssignment
                {
                    AssignmentId = 2,
                    EmployeeId = 2,
                    ProjectId = 2
                }
            );
        }
    }
}
