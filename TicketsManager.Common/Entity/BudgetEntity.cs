namespace TicketsManager.Common.Entity;
public class BudgetEntity
{
    public Guid BudgetId { get; set; }
    public string BudgetName { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public Guid UserId { get; set; }
}
