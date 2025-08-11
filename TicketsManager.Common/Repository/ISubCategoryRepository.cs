using Microsoft.EntityFrameworkCore;
using TicketsManager.Common.Entity;

namespace TicketsManager.Common.Repository;
public interface ISubCategoryRepository
{
    DbSet<SubCategoryEntity> SubCategories { get; set; }
}
