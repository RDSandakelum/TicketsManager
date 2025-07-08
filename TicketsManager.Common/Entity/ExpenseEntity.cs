namespace TicketsManager.Common.Entity;
public class ExpenseEntity
{
    public Guid ExpenseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset SpentDate { get; set; }
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
}
