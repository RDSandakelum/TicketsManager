using Microsoft.EntityFrameworkCore;
using TicketsManager.Business.Actions.Users;
using TicketsManager.Business.MappingConfig;
using TicketsManager.Common.Database;
using TicketsManager.Common.Services;
using TicketsManager.Common.Services.Definitions;
using TicketsManager.DataAccess.EFCustomizations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var services = builder.Services;

services.AddDbContext<TicketsManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketsManagerDbConnectionString"));
});

services.AddScoped<ITicketsManagerDbContext, TicketsManagerDbContext>();

services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingConfigurations>();
});

services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
});

services.AddScoped<IPasswordService, PasswordService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

