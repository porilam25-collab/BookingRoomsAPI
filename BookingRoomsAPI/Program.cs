using BookingRoomsAPI.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<BookingRoomsDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(BookingRoomsDbContext)));
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
