using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Model;
using TaskManagementAPI.Repositories.Interface;

namespace TaskManagementAPI.Repositories.Implement
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectTaskDbContext _context;
        public TaskRepository(ProjectTaskDbContext context)
        {
            _context = context;
        }

        public async Task<TaskObj?> GetTaskWithAssignmentsByIdAsync(int taskId)
        {
            return await _context.Tasks.Include(t => t.TaskAssignments)
                .FirstOrDefaultAsync(t => t.TaskObjId == taskId);
        }
    }
}
