namespace TicketsManager.Common.Entity;
public class BudgetAllocationEntity
{
    public Guid AllocationId { get; set; }
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public decimal Amount { get; set; } = 0;
}
