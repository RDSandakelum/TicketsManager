namespace TicketsManager.Common.Entity;
public class ExpenseEntity
{
    public Guid ExpenseId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; } = 0;
    public DateTimeOffset SpentDate { get; set; }
    public Guid BudgetId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public BudgetEntity Budget { get; set; }
    public CategoryEntity? Category { get; set; }
    public SubCategoryEntity? SubCategory { get; set; }
}
