using TaskManagementAPI.Data;
using TaskManagementAPI.Repositories.Interface;
using TaskManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementAPI.Repositories.Implement
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectTaskDbContext _context;
        public ProjectRepository(ProjectTaskDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectObj?> GetProjectWithMemberAndTaskByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectObjId == id && !p.IsDeleted);
        }
    }
}
