using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Model;

namespace TaskManagementAPI.Data
{
    public class ProjectTaskDbContext : DbContext
    {
        public ProjectTaskDbContext(DbContextOptions<ProjectTaskDbContext> options) : base(options)
        {
        }
        public DbSet<ProjectObj> Projects { get; set; }
        public DbSet<TaskObj> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
