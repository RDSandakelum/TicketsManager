using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface ICategoryRepository
{
    DbSet<CategoryEntity> Categories { get; set; }
}
