using TaskManagementAPI.Model;
namespace TaskManagementAPI.DTOs.ProjectObj
{
    public class ProjectUpdateDTO
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus? Status { get; set; }
    }
}
