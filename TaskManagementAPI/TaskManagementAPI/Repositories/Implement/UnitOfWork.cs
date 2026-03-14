using TaskManagementAPI.Data;
using TaskManagementAPI.Repositories.Interface;

namespace TaskManagementAPI.Repositories.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
            private readonly ProjectTaskDbContext _context;
    
            public UnitOfWork(ProjectTaskDbContext context)
            {
                _context = context;
            }
    
            public async Task<int> SaveChangesAsync()
            {
                return await _context.SaveChangesAsync();
            }
    
            public void Dispose()
            {
                _context.Dispose();
        }
    }
}
