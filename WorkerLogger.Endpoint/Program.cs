using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkerLogger.Application;
using WorkerLogger.Domain.Entities.ApplicationUsers;
using WorkerLogger.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireDigit = false;
    option.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<WorkerLoggerDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = "http://www.security.org",
        ValidIssuer = "http://www.security.org",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("biztonsagostitkoskod"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

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
