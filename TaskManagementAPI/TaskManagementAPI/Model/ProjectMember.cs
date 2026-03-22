namespace TaskManagementAPI.Model
{
    public class ProjectMember
    {
        public int ProjectMemberId { get; private set; } // Id của thành viên dự án
        public int ProjectId { get; private set; } // Id của dự án
        public ProjectObj? project { get; private set; } // Tham chiếu đến dự án
        public int UserId { get; private set; } // Id của người dùng
        public User? User { get; private set; } // Tham chiếu đến người dùng

        // Vai trò của thành viên trong dự án (ví dụ: "Developer", "Tester", "Manager")
        public string MemberRole { get; private set; } = string.Empty;

        // Constructor rỗng
        public ProjectMember() { }

        // Constructor có tham số
        public ProjectMember(int projectId, int userId, string memberRole)
        {
            ProjectId = projectId;
            UserId = userId;
            MemberRole = memberRole;
        }

        // Phương thức để cập nhật vai trò của thành viên trong dự án
        public void UpdateMemberRole(string? memberRole)
        {
            if (memberRole != null)
                MemberRole = memberRole;
        }
    }
}
