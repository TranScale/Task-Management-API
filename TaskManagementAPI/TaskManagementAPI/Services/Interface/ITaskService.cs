using TaskManagementAPI.DTOs.TaskObj;

namespace TaskManagementAPI.Services.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync();
        Task<TaskResponseDTO?> GetTaskByIdAsync(int id);
        Task<TaskResponseDTO> CreateTaskAsync(TaskCreateDTO taskCreateDTO);
        Task<TaskResponseDTO?> UpdateTaskAsync(int id, TaskUpdateDTO dto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
