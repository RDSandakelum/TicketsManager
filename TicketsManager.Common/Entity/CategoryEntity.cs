using TicketsManager.Common.Enum;

namespace TicketsManager.Common.Entity;
public class CategoryEntity
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public CategoryType CategoryType { get; set; }
    public Guid UserId { get; set; }
    public Guid BudgetTemplateId { get; set; }
}
