using TicketsManager.Common.Enum;

namespace TicketsManager.Common.Entity;
public class SubCategoryEntity
{
    public SubCategoryEntity()
    {
        Expenses = new HashSet<ExpenseEntity>();
    }
    public Guid SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
    public CategoryType SubCategoryType { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
    public ICollection<ExpenseEntity> Expenses { get; set; }
}
