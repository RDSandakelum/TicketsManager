namespace TicketsManager.Common.Database;
public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync();
}
