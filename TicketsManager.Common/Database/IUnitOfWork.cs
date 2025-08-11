namespace TicketsManager.Common.Database;
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}
