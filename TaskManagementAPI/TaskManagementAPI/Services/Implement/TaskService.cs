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
        private readonly IRepository<ProjectMember> _memrepo;
        private readonly ITaskRepository _taskrepository;

        public TaskService(IUnitOfWork unitOfWork, IRepository<TaskObj> _repository, IRepository<ProjectObj> _projectRepository, IRepository<ProjectMember> memrepo, ITaskRepository taskrepository)
        {
            _unitOfWork = unitOfWork;
            _taskrepo = _repository;
            _prorepo = _projectRepository;
            _memrepo = memrepo;
            _taskrepository = taskrepository;
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

        public async Task<bool> AddAsignment(int taskId, int userId) 
        {
            var task = await _taskrepo.GetByIdAsync(taskId);
            if (task == null) return false;

            var member = await _memrepo.GetAllAsync();
            var isMember = member.Any(m => m.ProjectId == task.ProjectId && m.UserId == userId);

            if (!isMember) return false;

            task.AddTaskAssignment(new TaskAssignment(taskId, userId));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsignment(int taskId, int userId)
        {
            var task = await _taskrepository.GetTaskWithAssignmentsByIdAsync(taskId);
            if (task == null) return false;

            var assignment = task.TaskAssignments.FirstOrDefault(a => a.UserId == userId);
            if (assignment == null) return false;
            task.RemoveTaskAssignment(assignment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTaskStatus(int id)
        {
            var task = await _taskrepo.GetByIdAsync(id);
            if (task == null)
                return false;
            task.UpdateTaskStatus();
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
