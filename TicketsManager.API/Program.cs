using Microsoft.EntityFrameworkCore;
using TicketsManager.Business.SecurityService;
using TicketsManager.Business.UserManagement;
using TicketsManager.DataAccess.Repositories;
using TicketsManager.DataAccess.TicketManagerDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserManagement, UserManagement>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<TicketsManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
