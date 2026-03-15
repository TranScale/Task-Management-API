using TaskManagementAPI.DTOs.TaskObj;
using TaskManagementAPI.Repositories.Interface;
using TaskManagementAPI.Services.Interface;
using TaskManagementAPI.Model;

namespace TaskManagementAPI.Services.Implement
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TaskObj> _taskrepo;
        private readonly IRepository<ProjectObj> _prorepo;

        public TaskService(IUnitOfWork unitOfWork, IRepository<TaskObj> _repository, IRepository<ProjectObj> _projectRepository)
        {
            _unitOfWork = unitOfWork;
            _taskrepo = _repository;
            _prorepo = _projectRepository;
        }

        public async Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskrepo.GetAllAsync();
            return tasks.Select(t => MapToResponseDTO(t)).ToList();
        }

        public async Task<TaskResponseDTO?> GetTaskByIdAsync(int id)
        {
            var task = await _taskrepo.GetByIdAsync(id);
            if (task == null)
                return null;
            return MapToResponseDTO(task);
        }

        public async Task<TaskResponseDTO> CreateTaskAsync(TaskCreateDTO taskCreateDTO)
        {
            var projectExists = await _prorepo.GetByIdAsync(taskCreateDTO.ProjectId);
            if (projectExists == null)
            {
                throw new Exception("Dự án không tồn tại.");
            }
            var newTask = new TaskObj(taskCreateDTO.ProjectId, taskCreateDTO.TaskName, taskCreateDTO.TaskDescription, taskCreateDTO.StartDate);
            await _taskrepo.AddAsync(newTask);
            await _unitOfWork.SaveChangesAsync();
            return MapToResponseDTO(newTask);
        }

        public async Task<TaskResponseDTO?> UpdateTaskAsync(int id, TaskUpdateDTO taskUpdateDTO)
        {
            var task = await _taskrepo.GetByIdAsync(id);
            if (task == null)
                return null;
            task.UpdateTask(taskUpdateDTO.TaskName, taskUpdateDTO.TaskDescription, taskUpdateDTO.StartDate, taskUpdateDTO.EndDate);
            _taskrepo.Update(task);
            await _unitOfWork.SaveChangesAsync();
            return MapToResponseDTO(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskrepo.GetByIdAsync(id);
            if (task == null)
                return false;
            _taskrepo.Delete(task);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private TaskResponseDTO MapToResponseDTO(TaskObj task)
        {
            return new TaskResponseDTO
            {
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                CreateTime = task.CreatedAt,
                EndTime = task.DueDate,
                IsCompleted = task.IsCompleted
            };
        }
    }
}
