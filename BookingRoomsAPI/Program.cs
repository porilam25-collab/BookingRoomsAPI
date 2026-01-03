using BookingRoomsAPI.DataAccess.PostgreSQL;
using BookingRoomsAPI.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var secretKey = configuration[$"{nameof(JwtSettings)}:SecretKey"] 
    ?? throw new InvalidOperationException("Jwt token not configured");

builder.Services.Configure<JwtSettings>(
    configuration.GetSection(nameof(JwtSettings)));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddDbContext<BookingRoomsDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(BookingRoomsDbContext)));
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
