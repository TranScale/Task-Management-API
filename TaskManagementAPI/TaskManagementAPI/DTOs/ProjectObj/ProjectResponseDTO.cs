using TaskManagementAPI.Model;

namespace TaskManagementAPI.DTOs.ProjectObj
{
    public class ProjectResponseDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
