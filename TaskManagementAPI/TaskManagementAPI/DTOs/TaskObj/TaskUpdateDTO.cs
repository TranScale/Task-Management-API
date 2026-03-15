namespace TaskManagementAPI.DTOs.TaskObj
{
    public class TaskUpdateDTO
    {
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
