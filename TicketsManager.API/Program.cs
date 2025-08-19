using Microsoft.EntityFrameworkCore;
using TicketsManager.Business.Actions.Users;
using TicketsManager.Business.MappingConfig;
using TicketsManager.Business.Services;
using TicketsManager.Common.Database;
using TicketsManager.Common.Services;
using TicketsManager.Common.Services.Definitions;
using TicketsManager.DataAccess.EFCustomizations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using TicketsManager.Business.Validators.Users;
using TicketsManager.API.ExtentionsForStartup;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        var services = builder.Services;

        services.AddDbContext<TicketsManagerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("TicketsManagerDbConnectionString"));
        });

        services.AddScoped<ITicketsManagerDbContext, TicketsManagerDbContext>();

        services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly, includeInternalTypes: true);

        //services.AddCustomExceptionsHandlers();
        services.AddCustomExceptionsHandlers();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"]!)),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingConfigurations>();
        });

        services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
        services.AddScoped<TokenService>();

        services.AddMediatR(configurations =>
        {
            configurations.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
        });

        services.AddScoped<IPasswordService, PasswordService>();

        var app = builder.Build();

        app.UseExceptionHandler(option => { });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}