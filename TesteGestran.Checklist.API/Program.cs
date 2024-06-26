using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TesteGestran.Checklist.Domain.Interfaces.Infra;
using TesteGestran.Checklist.Domain.Interfaces.Service;
using TesteGestran.Checklist.Domain.Mapping;
using TesteGestran.Checklist.Infra.Repositories.Auth;
using TesteGestran.Checklist.Infrastructure.Repositories;
using TesteGestran.Checklist.Service.Services.Auth;
using TesteGestran.Checklist.Services;
using TesteGestran.Checklist.Services.Utils;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Clear and add console logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.AutomaticValidationEnabled = true;  // Assegura que a validação automática esteja habilitada
    });


// Ensure validators are automatically validating actions
builder.Services.AddFluentValidationAutoValidation();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

// Register repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IChecklistRepository, ChecklistRepository>();

// Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChecklistService, ChecklistService>();

// Register utility classes
builder.Services.AddSingleton<JwtTokenGenerator>();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireExecutorRole", policy => policy.RequireRole("Executor"));
    options.AddPolicy("RequireSupervisorRole", policy => policy.RequireRole("Supervisor"));
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Middleware to intercept 403 Forbidden responses
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            success = false,
            errors = new[] { "Somente supervisores podem finalizar o checklist." }
        }));
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
