using TicketsManager.Common.Enum;

namespace TicketsManager.Common.Entity;
public class SubCategoryEntity
{
    public Guid SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
    public CategoryType SubCategoryType { get; set; }
    public Guid UserId { get; set; }
    public Guid BudgetTemplateId {  get; set; }
    public Guid CategoryId { get; set; }
}
