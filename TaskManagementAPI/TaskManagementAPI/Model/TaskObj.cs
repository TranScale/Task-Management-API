namespace TaskManagementAPI.Model
{
    public class TaskObj
    {
        public int TaskObjId { get; private set; } // Id của công việc
        public int ProjectId { get; private set; } // Id của dự án mà công việc thuộc về
        public ProjectObj? Project { get; private set; } // Dự án mà công việc thuộc về
        public string TaskName { get; private set; } = string.Empty; // Tên của công việc
        public string TaskDescription { get; private set; } = string.Empty; // Mô tả về công việc
        public bool IsCompleted { get; private set; } = false; // Trạng thái hoàn thành của công việc
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow; // Ngày tạo công việc
        public DateTime? DueDate { get; private set; } // Ngày hết hạn của công việc

        // Danh sách người được giao công việc
        public ICollection<TaskAssignment> TaskAssignments { get; private set; } = new List<TaskAssignment>();

        // Constructor rỗng
        public TaskObj() { }

        // Constructor có tham số
        public TaskObj(int projectId, string taskName, string? taskDescription, DateTime? createAt)
        {
            ProjectId = projectId;
            TaskName = taskName;
            if(taskDescription != null)
                TaskDescription = taskDescription;
            if(createAt != null)
                CreatedAt = createAt.Value;
            else 
                CreatedAt = DateTime.UtcNow;
        }

        // Phương thức để cập nhật thông tin công việc
        public void UpdateTask(string? taskName, string? taskDescription,DateTime? createAt, DateTime? dueDate)
        {
            if (taskName != null)
                TaskName = taskName;
            if (taskDescription != null)
                TaskDescription = taskDescription;
            if (createAt != null)
                CreatedAt = createAt.Value;
            if (dueDate != null)
                DueDate = dueDate.Value;
        }

        // Phương thức để cập nhật trạng thái hoàn thành của công việc
        public void UpdateTaskStatus(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        // Phương thức để thêm người được giao công việc
        public void AddTaskAssignment(TaskAssignment taskAssignment)
        {
            TaskAssignments.Add(taskAssignment);
        }

    }
}
