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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.User)
                .WithMany()
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany()
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // 1. Tạo sẵn 3 Người dùng (User)
            modelBuilder.Entity<User>().HasData(
                new { UserId = 1, UserName = "Nguyễn Văn A (Manager)" },
                new { UserId = 2, UserName = "Trần Thị B (Developer)" },
                new { UserId = 3, UserName = "Lê Văn C (Tester)" }
            );

            // 2. Tạo sẵn 2 Dự án (ProjectObj)
            modelBuilder.Entity<ProjectObj>().HasData(
                new
                {
                    ProjectObjId = 1,
                    ProjectName = "Dự án Website E-commerce",
                    ProjectDescription = "Làm trang web bán hàng cho đối tác",
                    StartDate = DateTime.UtcNow.AddDays(-30), // Bắt đầu 30 ngày trước
                    Status = ProjectStatus.InProgress,
                    IsDeleted = false
                },
                new
                {
                    ProjectObjId = 2,
                    ProjectName = "Dự án Mobile App React Native",
                    ProjectDescription = "App quản lý nhân sự",
                    StartDate = DateTime.UtcNow.AddDays(10), // Bắt đầu 10 ngày nữa
                    Status = ProjectStatus.NotStarted,
                    IsDeleted = false
                }
            );

            // 3. Phân công Thành viên vào Dự án (ProjectMember)
            modelBuilder.Entity<ProjectMember>().HasData(
                // Dự án 1 có A làm Manager, B làm Dev
                new { ProjectMemberId = 1, ProjectId = 1, UserId = 1, MemberRole = "Project Manager" },
                new { ProjectMemberId = 2, ProjectId = 1, UserId = 2, MemberRole = "Backend Developer" },

                // Dự án 2 có B làm Lead, C làm Tester
                new { ProjectMemberId = 3, ProjectId = 2, UserId = 2, MemberRole = "Technical Lead" },
                new { ProjectMemberId = 4, ProjectId = 2, UserId = 3, MemberRole = "QA/Tester" }
            );

            // 4. Tạo sẵn Công việc (TaskObj)
            modelBuilder.Entity<TaskObj>().HasData(
                // Task của dự án 1
                new
                {
                    TaskObjId = 1,
                    ProjectId = 1,
                    TaskName = "Thiết kế Database",
                    TaskDescription = "Vẽ sơ đồ ERD và viết script SQL",
                    IsCompleted = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-28),
                    DueDate = DateTime.UtcNow.AddDays(-20)
                },
                new
                {
                    TaskObjId = 2,
                    ProjectId = 1,
                    TaskName = "Code API Đăng nhập",
                    TaskDescription = "Sử dụng JWT để làm tính năng Authentication",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-19),
                    DueDate = DateTime.UtcNow.AddDays(5)
                },

                // Task của dự án 2
                new
                {
                    TaskObjId = 3,
                    ProjectId = 2,
                    TaskName = "Setup môi trường Mobile",
                    TaskDescription = "Cài đặt Android Studio và Xcode",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(12)
                }
            );

            // 5. Giao Task cho thành viên (TaskAssignment)
            modelBuilder.Entity<TaskAssignment>().HasData(
                // Giao Task 1 cho bạn A
                new { TaskAssignmentId = 1, TaskId = 1, UserId = 1 },
                // Giao Task 2 cho bạn B
                new { TaskAssignmentId = 2, TaskId = 2, UserId = 2 },
                // Giao Task 3 cho bạn B
                new { TaskAssignmentId = 3, TaskId = 3, UserId = 2 }
            );
        }
    }
}
