using TaskManagementAPI.DTOs.ProjectMember;
using TaskManagementAPI.Model;

namespace TaskManagementAPI.Services.Interface
{
    public interface IProjectMemberService
    {
        Task<IEnumerable<ProjectMemberResponseDTO>> GetProjectMembersAsync(int projectId);
        Task<bool> AddProjectMemberAsync(int projectId, ProjectMember projectMember);
        Task<bool> RemoveProjectMemberAsync(int projectId, int userId);
    }
}
