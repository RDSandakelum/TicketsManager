using Microsoft.EntityFrameworkCore;
using TicketsManager.DataAccess.TicketsManagerDBContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var services = builder.Services;

services.AddDbContext<TicketsManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketsManagerDbConnectionString"));
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

