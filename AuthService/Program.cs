using System.Text;
using AuthService.Data;
using AuthService.Extensions;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on all interfaces
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

// Check for migration command
var applyMigrations = args.Contains("--apply-migrations");

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Add database context with SQL Server instead of SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Register token validation service
builder.Services.AddScoped<TokenValidationService>();

// Register token cleanup background service
builder.Services.AddHostedService<TokenCleanupService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? 
    throw new InvalidOperationException("JWT SecretKey not found in configuration.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

// Add API controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Apply migrations if requested
if (applyMigrations)
{
    Console.WriteLine("Applying database migrations...");
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    Console.WriteLine("Migrations applied successfully!");
    return;
}

// Configure the HTTP request pipeline.
// Use built-in exception handler instead of custom middleware
app.UseExceptionHandler(exceptionHandlerApp => 
{
    exceptionHandlerApp.Run(async context => 
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        
        var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (feature != null)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(feature.Error, "Unhandled exception");
            
            // Return sanitized error message
            string message = "An unexpected error occurred. Please try again later.";
            
            // Check for specific exception types to provide appropriate messages
            if (feature.Error is Microsoft.Data.SqlClient.SqlException)
            {
                message = "Database service is currently unavailable. Please try again later.";
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            }
            else if (feature.Error is Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                message = "A database error occurred while processing your request.";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            else if (feature.Error is UnauthorizedAccessException)
            {
                message = "You are not authorized to perform this action.";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            
            await context.Response.WriteAsJsonAsync(new { message });
        }
    });
});

if (app.Environment.IsDevelopment())
{
    // Don't use DeveloperExceptionPage as it shows too much information
    // app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// These must be in this order
app.UseTokenValidation(); // Add our custom token validation middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
