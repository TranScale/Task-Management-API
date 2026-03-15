namespace TaskManagementAPI.DTOs.ProjectMember
{
    public class ProjectMemberResponseDTO
    {
        public int UserId { get; set; } // Id của người dùng
        public string UserName { get; set; } = string.Empty; // Tên người dùng
        public string MemberRole { get; set; } = string.Empty; // Vai trò của thành viên trong dự án
    }
}
