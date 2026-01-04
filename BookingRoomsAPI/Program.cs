using BookingRoomsAPI.DataAccess.PostgreSQL;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Repositories;
using BookingRoomsAPI.DataAccess.PostgreSQL.Abstractions.Services;
using BookingRoomsAPI.DataAccess.PostgreSQL.Repositories;
using BookingRoomsAPI.Domain.Entities;
using BookingRoomsAPI.Options;
using BookingRoomsAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["tasty-cookies"];

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddDbContext<BookingRoomsDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(BookingRoomsDbContext)));
});

builder.Services.AddScoped<PasswordHasher<User>>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IRoomsRepository, RoomsRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
