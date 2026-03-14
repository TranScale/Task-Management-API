using TaskManagementAPI.DTOs.ProjectObj;
using TaskManagementAPI.Model;
using TaskManagementAPI.Repositories.Interface;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services.Implement
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProjectObj> _prorepo;

        public ProjectService(IUnitOfWork unitOfWork, IRepository<ProjectObj> prorepo)
        {
            _unitOfWork = unitOfWork;
            _prorepo = prorepo;
        }

        public async Task<IEnumerable<ProjectResponseDTO>> GetAllProjectsAsync()
        {
            var projects = await _prorepo.GetAllAsync();
            return projects.Where(p => p.IsDeleted == false).Select(p => MapToResponseDTO(p)).ToList();
        }

        public async Task<ProjectResponseDTO?> GetProjectByIdAsync(int id)
        {
            var project = await _prorepo.GetByIdAsync(id);
            if (project == null || project.IsDeleted)
                return null;
            return MapToResponseDTO(project);
        }

        public async Task<ProjectResponseDTO> CreateProjectAsync(ProjectCreateDTO projectCreateDTO)
        {
            var project = new ProjectObj(projectCreateDTO.ProjectName, projectCreateDTO.ProjectDescription, projectCreateDTO.StartDate, projectCreateDTO.EndDate);
            await _prorepo.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
            return MapToResponseDTO(project);
        }

        public async Task<ProjectResponseDTO?> UpdateProjectAsync(int id, ProjectUpdateDTO projectUpdateDTO)
        {
            var project = await _prorepo.GetByIdAsync(id);
            if (project == null || project.IsDeleted)
                return null;
            project.UpdateProject(projectUpdateDTO.ProjectName, projectUpdateDTO.ProjectDescription, projectUpdateDTO.StartDate, projectUpdateDTO.EndDate, projectUpdateDTO.Status);
            _prorepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return MapToResponseDTO(project);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _prorepo.GetByIdAsync(id);
            if(project == null || project.IsDeleted)
                return false;
            project.MarkAsDeleted();
            _prorepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        private ProjectResponseDTO MapToResponseDTO(ProjectObj project)
        {
            return new ProjectResponseDTO
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription ?? "Không có mô tả",
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };
        }
    }
}
