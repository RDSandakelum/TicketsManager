namespace TicketsManager.Common.Entity;
public class BudgetEntity
{
    public BudgetEntity()
    {
        BudgetAllocations = new HashSet<BudgetAllocationEntity>();
        BudgetExpenses = new HashSet<ExpenseEntity>();
    }
    public Guid BudgetId { get; set; }
    public string BudgetName { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public Guid UserId { get; set; }
    public Guid BudgetTemplateId { get; set; }
    public UserEntity User { get; set; }
    public BudgetTemplateEntity BudgetTemplate { get; set; }
    public ICollection<BudgetAllocationEntity> BudgetAllocations { get; set; } 
    public ICollection<ExpenseEntity> BudgetExpenses { get; set; }
}
