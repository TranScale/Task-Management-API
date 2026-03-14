using TaskManagementAPI.DTOs.ProjectObj;
using TaskManagementAPI.Model;

namespace TaskManagementAPI.Services.Interface
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDTO>> GetAllProjectsAsync();
        Task<ProjectResponseDTO?> GetProjectByIdAsync(int id);
        Task<ProjectResponseDTO> CreateProjectAsync(ProjectCreateDTO projectCreateDTO);
        Task<ProjectResponseDTO?> UpdateProjectAsync(int id, ProjectUpdateDTO dto);
        Task<bool> DeleteProjectAsync(int id);
    }
}
