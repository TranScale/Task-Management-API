using TaskManagementAPI.DTOs.TaskObj;
using TaskManagementAPI.Model;

namespace TaskManagementAPI.Services.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync();
        Task<TaskResponseDTO?> GetTaskByIdAsync(int id);
        Task<TaskResponseDTO> CreateTaskAsync(TaskCreateDTO taskCreateDTO);
        Task<TaskResponseDTO?> UpdateTaskAsync(int id, TaskUpdateDTO dto);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> AddAsignment(int taskId, int userId);
        Task<bool> DeleteAsignment(int taskId, int userId);
        Task<bool> UpdateTaskStatus(int id);
    }
}
