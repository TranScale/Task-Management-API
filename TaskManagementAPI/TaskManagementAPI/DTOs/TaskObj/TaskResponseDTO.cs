namespace TaskManagementAPI.DTOs.TaskObj
{
    public class TaskResponseDTO
    {
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
