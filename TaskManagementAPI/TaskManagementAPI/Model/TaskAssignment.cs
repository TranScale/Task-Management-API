namespace TaskManagementAPI.Model
{
    public class TaskAssignment
    {
        public int TaskAssignmentId { get; private set; } // Id của việc giao công việc
        public int TaskId { get; private set; } // Id của công việc được giao
        public TaskObj? task { get; private set; } // Tham chiếu đến công việc được giao
        public int UserId { get; private set; } // Id của người được giao công việc
        public User? user { get; private set; } // Tham chiếu đến người được giao công việc

        // Constructor rỗng
        public TaskAssignment() { }

        // Constructor có tham số
        public TaskAssignment(int taskId, int userId)
        {
            TaskId = taskId;
            UserId = userId;
        }
    }
}
