using TaskManagementAPI.DTOs.ProjectMember;
using TaskManagementAPI.DTOs.ProjectObj;
using TaskManagementAPI.DTOs.TaskObj;
using TaskManagementAPI.Model;
using TaskManagementAPI.Repositories.Interface;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services.Implement
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProjectObj> _prorepo;
        private readonly IProjectRepository _projectrepo;

        public ProjectService(IUnitOfWork unitOfWork, IRepository<ProjectObj> prorepo, IProjectRepository projectrepo)
        {
            _unitOfWork = unitOfWork;
            _prorepo = prorepo;
            _projectrepo = projectrepo;
        }

        public async Task<IEnumerable<ProjectResponseDTO>> GetAllProjectsAsync()
        {
            var projects = await _prorepo.GetAllAsync();
            return projects.Where(p => !p.IsDeleted).Select(p => MapToResponseDTO(p)).ToList();
        }

        public async Task<ProjectDetailDTO?> GetProjectByIdAsync(int id)
        {
            var project = await _projectrepo.GetProjectWithMemberAndTaskByIdAsync(id);
            if (project == null || project.IsDeleted)
                return null;
            return MapToDetailDTO(project);
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

        //public async Task<bool> AddProjectMember(int projectId, ProjectMember projectMember)
        //{
        //    var project = await _projectrepo.GetProjectWithMemberAndTaskByIdAsync(projectId);
        //    if (project == null || project.IsDeleted)
        //        return false;
        //    if (project.ProjectMembers.Any(m => m.UserId == projectMember.UserId))
        //        return false;

        //    project.AddProjectMember(projectMember);
        //    await _unitOfWork.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> RemoveProjectMember(int projectId, int userId)
        //{
        //    var project = await _projectrepo.GetProjectWithMemberAndTaskByIdAsync(projectId);
        //    if (project == null || project.IsDeleted)
        //        return false;
        //    var member = project.ProjectMembers.FirstOrDefault(m => m.UserId == userId);
        //    if (member == null)
        //        return false;
        //    project.RemoveProjectMember(userId);
        //    await _unitOfWork.SaveChangesAsync();
        //    return true;
        //}

        private ProjectResponseDTO MapToResponseDTO(ProjectObj project)
        {
            return new ProjectResponseDTO
            {
                ProjectId = project.ProjectObjId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription ?? "Không có mô tả",
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };
        }

        private ProjectDetailDTO MapToDetailDTO(ProjectObj project)
        {
            return new ProjectDetailDTO
            {
                ProjectId = project.ProjectObjId,
                ProjectName = project.ProjectName,
                ProjectDescription = project.ProjectDescription ?? "Không có mô tả",
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                Tasks = project.Tasks?.Select(t => new TaskResponseDTO
                {
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription ?? "Không có mô tả",
                    CreateTime = t.CreatedAt,
                    EndTime = t.DueDate,
                    IsCompleted = t.IsCompleted
                }).ToList() ?? new List<TaskResponseDTO>(),
                Members = project.ProjectMembers?.Select(m => new ProjectMemberResponseDTO
                {
                    UserId = m.UserId,
                    UserName = m.User?.UserName ?? "Not Found",
                    MemberRole = m.MemberRole
                }).ToList() ?? new List<ProjectMemberResponseDTO>()
            };
        }

    }
}
