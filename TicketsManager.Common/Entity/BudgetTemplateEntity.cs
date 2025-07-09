namespace TicketsManager.Common.Entity;
public class BudgetTemplateEntity
{
    public BudgetTemplateEntity()
    {
        Categories = new HashSet<CategoryEntity>();
    }
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; }
    public Guid UserId { get; set; }
    public ICollection<CategoryEntity> Categories { get; set; }
    public UserEntity User { get; set; }
}
