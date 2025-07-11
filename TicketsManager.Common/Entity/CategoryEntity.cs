using TicketsManager.Common.Enum;

namespace TicketsManager.Common.Entity;
public class CategoryEntity
{
    public CategoryEntity()
    {
        SubCategories = new HashSet<SubCategoryEntity>();
    }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public CategoryType CategoryType { get; set; }
    public Guid BudgetTemplateId { get; set; }
    public ICollection<SubCategoryEntity> SubCategories { get; set; }
    public BudgetTemplateEntity BudgetTemplate { get; set; }
}
