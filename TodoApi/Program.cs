using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
var conectToDB=builder.Configuration.GetConnectionString("ToDoDB");
builder.Services.AddDbContext<ToDoDbContext>(options=>options.UseMySql(conectToDB,ServerVersion.Parse("8.0.46-mysql")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
