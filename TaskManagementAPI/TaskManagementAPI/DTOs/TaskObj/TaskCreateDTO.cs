namespace TaskManagementAPI.DTOs.TaskObj
{
    public class TaskCreateDTO
    {
        public int ProjectId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string? TaskDescription { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
