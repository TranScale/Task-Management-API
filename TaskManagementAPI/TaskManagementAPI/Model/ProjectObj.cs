namespace TaskManagementAPI.Model
{
    public class ProjectObj
    {
        public int ProjectId { get; private set; } //Id của dự án
        public string ProjectName { get; private set; } = string.Empty; // Tên của dự án
        public string ProjectDescription { get; private set; } = string.Empty; // Mô tả về dự án
        public DateTime StartDate { get; private set; } // Ngày bắt đầu của dự án
        public DateTime? EndDate { get; private set; } // Ngày kết thúc của dự án

        // Trạng thái hoàn thành của dự án
        public ProjectStatus Status { get; private set; } = ProjectStatus.NotStarted;

        //Trạng thái xóa mềm của dự án
        public bool IsDeleted { get; private set; } = false;

        //Danh sách thành viên tham gia dự án
        public ICollection<ProjectMember> ProjectMembers { get; private set; } = new List<ProjectMember>();

        //Danh sách công việc liên quan đến dự án
        //public ICollection<TaskObj> Tasks { get; private set; } = new List<TaskObj>();

        //Constructor rỗng
        public ProjectObj() { }

        //Constructor có tham số
        public ProjectObj(string projectName, string? projectDescription, DateTime startDate, DateTime? endDate)
        {
            ProjectName = projectName;
            if (projectDescription != null)
                ProjectDescription = projectDescription;
            StartDate = startDate;
            EndDate = endDate;
        }

        // Phương thức để cập nhật thông tin dự án
        public void UpdateProject(string? projectName, string? projectDescription, DateTime? startDate, DateTime? endDate, ProjectStatus? status)
        {
            if(projectName != null)
                ProjectName = projectName;

            if(projectDescription != null)
                ProjectDescription = projectDescription;

            if(startDate != null)
                StartDate = startDate.Value;

            if(endDate != null)
                EndDate = endDate.Value;

            if (status != null)
                Status = status.Value;
        }

        // Phương thức để cập nhật trạng thái của dự án
        public void UpdateProjectStatus(ProjectStatus status)
        {
            Status = status;
        }

        // Phương thức để đánh dấu dự án là đã xóa mềm
        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

    }
}
