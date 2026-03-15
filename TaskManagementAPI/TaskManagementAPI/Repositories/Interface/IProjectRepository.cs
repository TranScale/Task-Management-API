using TaskManagementAPI.Model;

namespace TaskManagementAPI.Repositories.Interface
{
    public interface IProjectRepository
    {
        Task<ProjectObj?> GetProjectWithMemberAndTaskByIdAsync(int id);
    }
}
