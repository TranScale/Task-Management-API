using TaskManagementAPI.Model;

namespace TaskManagementAPI.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<TaskObj?> GetTaskWithAssignmentsByIdAsync(int taskId);
    }
}
