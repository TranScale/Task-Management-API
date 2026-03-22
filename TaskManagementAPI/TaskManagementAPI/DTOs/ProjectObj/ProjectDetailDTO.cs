using TaskManagementAPI.DTOs.TaskObj;
using TaskManagementAPI.DTOs.ProjectMember;
namespace TaskManagementAPI.DTOs.ProjectObj
{
    public class ProjectDetailDTO : ProjectResponseDTO
    {
        public ICollection<TaskResponseDTO>? Tasks { get; set; } = new List<TaskResponseDTO>();
        public ICollection<ProjectMemberResponseDTO>? Members { get; set; } = new List<ProjectMemberResponseDTO>();
    }
}
