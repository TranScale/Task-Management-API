namespace TaskManagementAPI.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
