using TaskManagementAPI.DTOs.ProjectMember;
using TaskManagementAPI.Model;
using TaskManagementAPI.Repositories.Interface;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services.Implement
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProjectMember> _pmrepo;
        private readonly IRepository<ProjectObj> _prorepo;
        private readonly IProjectRepository _projectRepository;

        public ProjectMemberService(IUnitOfWork unitOfWork, IRepository<ProjectMember> pmrepo, IRepository<ProjectObj> prorepo, IProjectRepository projectRepository)
        {
            _unitOfWork = unitOfWork;
            _pmrepo = pmrepo;
            _prorepo = prorepo;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectMemberResponseDTO>> GetProjectMembersAsync(int projectId)
        {
            var project = await _projectRepository.GetProjectWithMemberAndTaskByIdAsync(projectId);
            if (project == null || project.IsDeleted)
            {
                throw new Exception("Project not found");
            }
            var projectMembers = project.ProjectMembers;
            return projectMembers.Select(pm => MapToResponseDTO(pm)).ToList();
        }

        public async Task<bool> AddProjectMemberAsync(int projectId, ProjectMember projectMember)
        {
            var project = await _projectRepository.GetProjectWithMemberAndTaskByIdAsync(projectId);
            if (project == null || project.IsDeleted)
            {
                return false;
            }
            if(project.ProjectMembers.Any(pm => pm.UserId == projectMember.UserId))
            {
                return false;
            }

            project.AddProjectMember(projectMember);
            _prorepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProjectMemberAsync(int projectId, int userId)
        {
            var project = await _projectRepository.GetProjectWithMemberAndTaskByIdAsync(projectId);
            if (project == null || project.IsDeleted)
            {
                return false;
            }
            var projectMember = project.ProjectMembers.FirstOrDefault(pm => pm.UserId == userId);
            if (projectMember == null)
            {
                return false;
            }
            project.RemoveProjectMember(userId);
            _prorepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProjectMemberRoleAsync(int projectId, int userId, string newRole)
        {
            var project = await _projectRepository.GetProjectWithMemberAndTaskByIdAsync(projectId);
            if (project == null || project.IsDeleted)
                return false;
            var member = project.ProjectMembers.FirstOrDefault(pm => pm.UserId == userId);
            if (member == null)
                return false;
            member.UpdateMemberRole(newRole);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        private ProjectMemberResponseDTO MapToResponseDTO(ProjectMember projectMember)
        {
            return new ProjectMemberResponseDTO
            {
                UserId = projectMember.UserId,
                UserName = projectMember.User?.UserName ?? string.Empty,
                MemberRole = projectMember.MemberRole
            };
        }

    }
}
