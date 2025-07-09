namespace TicketsManager.Common.Entity;
public class BudgetAllocationEntity
{
    public Guid AllocationId { get; set; }
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public decimal Amount { get; set; } = 0;
    public BudgetEntity Budget { get; set; }
    public CategoryEntity Category { get; set; }
    public SubCategoryEntity? SubCategory { get; set; }
}
