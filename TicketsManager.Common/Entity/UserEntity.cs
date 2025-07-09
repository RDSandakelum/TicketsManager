namespace TicketsManager.Common.Entity;
public class UserEntity
{
    public UserEntity()
    {
        BudgetTemplates = new HashSet<BudgetTemplateEntity>();
        Budgets = new HashSet<BudgetEntity>();
        Expenses = new HashSet<ExpenseEntity>();
    }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public string NormalizedUsername { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public string PasswordHash { get; set; }    
    public string PasswordSalt { get; set; }
    public ICollection<BudgetTemplateEntity> BudgetTemplates { get; set; }
    public ICollection<BudgetEntity> Budgets { get; set; }
    public ICollection<ExpenseEntity> Expenses { get; set; }
}
