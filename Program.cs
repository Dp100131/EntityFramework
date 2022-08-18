using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectEF.Contexts;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TaskContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddSqlServer<TaskContext>(builder.Configuration.GetConnectionString("cnTasks"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TaskContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tasks", async([FromServices] TaskContext dbContext) =>
{
    return Results.Ok(dbContext.Tasks.Include(p => p.Category).Where(p => p.PriorityTask == projectEF.Models.Priority.Alta));
});

app.MapPost("/api/tasks", async([FromServices] TaskContext dbContext, [FromBody] projectEF.Models.Task task) =>
{
    task.TaskId = Guid.NewGuid();
    task.CreationDate = DateTime.Now;
    await dbContext.AddAsync(task);

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/api/tasks", async([FromServices] TaskContext dbContext, [FromBody] projectEF.Models.Task task, [FromRoute] Guid id) =>
{
    var taskActual = dbContext.Tasks.Find(id);

    if(taskActual != null){

        taskActual.CategoryId = task.CategoryId;
        taskActual.Title = task.Title;
        taskActual.PriorityTask = task.PriorityTask;
        taskActual.Description = taskActual.Description;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
        
    }

    return Results.NotFound();

    
});

app.Run();
