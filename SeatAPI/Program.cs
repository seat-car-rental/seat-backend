using SeatAPI.Data;
using Microsoft.EntityFrameworkCore;
using SeatAPI.Services.Users;
using SeatAPI.Services.Auth.PasswordHasher;
using SeatAPI.Services.Auth.UserAuth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IUserService, UserService>(); // User Service
builder.Services.AddScoped<IUserAuth, UserAuth>(); // User Auth
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>(); // Password Hashing Service


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
