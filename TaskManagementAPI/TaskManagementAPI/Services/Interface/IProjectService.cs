using TaskManagementAPI.DTOs.ProjectObj;
using TaskManagementAPI.Model;

namespace TaskManagementAPI.Services.Interface
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDTO>> GetAllProjectsAsync();
        Task<ProjectDetailDTO?> GetProjectByIdAsync(int id);
        Task<ProjectResponseDTO> CreateProjectAsync(ProjectCreateDTO projectCreateDTO);
        Task<ProjectResponseDTO?> UpdateProjectAsync(int id, ProjectUpdateDTO dto);
        Task<bool> DeleteProjectAsync(int id);
        //Task<bool> AddProjectMember(int projectId, ProjectMember projectMember);
        //Task<bool> RemoveProjectMember(int projectId, int userId);
    }
}
