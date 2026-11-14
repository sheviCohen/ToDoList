using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);
var conectToDB=builder.Configuration.GetConnectionString("ToDoDB");
builder.Services.AddDbContext<ToDoDbContext>(options=>options.UseMySql(conectToDB,ServerVersion.Parse("8.0.46-mysql")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/items",async(ToDoDbContext myDB,Item item)=>
{
    myDB.Items.Add(item);
    await myDB.SaveChangesAsync();
    return Results.Created($"/items/{item.Id}",item);
});

app.MapGet("/items",async(ToDoDbContext myDB)=>await myDB.Items.AsNoTracking().ToListAsync());

app.MapDelete("/items/{id}", async(int id,ToDoDbContext myDB,Item item)=>
{
    var task= await myDB.Items.FindAsync(id);
    if(task is null) return Results.NotFound();

    myDB.Items.Remove(task);
    await myDB.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("/items/{id}", async(int id, ToDoDbContext myDB) =>
{
    var task = await myDB.Items.FindAsync(id);
    if (task is null) return Results.NotFound();

    task.IsComplete = task.IsComplete;
    task.Name = task.Name?? task.Name;

    await myDB.SaveChangesAsync();
    return Results.NoContent();
});


app.Run();
