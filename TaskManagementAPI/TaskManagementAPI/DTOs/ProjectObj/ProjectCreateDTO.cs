using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs.ProjectObj
{
    public class ProjectCreateDTO
    {
        [Required(ErrorMessage = "Tên dự án không được để trống")]
        public string ProjectName { get; set; } = string.Empty;
        public string? ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
