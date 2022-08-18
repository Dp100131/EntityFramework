using Microsoft.EntityFrameworkCore;
using projectEF.Models;

namespace projectEF.Contexts
{
    public class TaskContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public TaskContext (DbContextOptions<TaskContext> options) :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> categoriesInit = new List<Category>();

            categoriesInit.Add(new Category(){
                CategoryId = Guid.Parse("bc4d1e59-4558-48b7-ba66-9c61172ca8a5"),
                Name = "Pending activities",
                Description = "Must be done urgently.",
                Weight = 100
            });

            categoriesInit.Add(new Category(){
                CategoryId = Guid.Parse("bc4d1e59-4558-48b7-ba66-9c61172ca8a4"),
                Name = "Personal activities",
                Description = "What I do.",
                Weight = 50
            });

            modelBuilder.Entity<Category>(category => 
            {

                category.ToTable("Category");
                category.HasKey(p => p.CategoryId);
                category.Property(p => p.Name).IsRequired().HasMaxLength(150);
                category.Property(p => p.Description).IsRequired(false);
                category.Property(p => p.Weight);
                category.HasData(categoriesInit);

            });

            List<Models.Task> taskInit = new List<Models.Task>();

            taskInit.Add(new Models.Task(){

                TaskId = Guid.Parse("bc4d1e59-4558-48b7-ba66-9c61172ca823"),
                CategoryId = Guid.Parse("bc4d1e59-4558-48b7-ba66-9c61172ca8a5"),
                Title = "Study English",
                Description = "Study English every Day.",
                PriorityTask = Priority.Alta,
                CreationDate = DateTime.Now

            });

            taskInit.Add(new Models.Task(){

                TaskId = Guid.Parse("bc4d1e59-4558-48b7-ba66-9c61172ca553"),
                CategoryId = Guid.Parse("bc4d1e59-4558-48b7-ba66-9c61172ca8a4"),
                Title = "Finish watching movie on HBO",
                Description = "Finish watch Steve Universe.",
                PriorityTask = Priority.Alta,
                CreationDate = DateTime.Now

            });

           modelBuilder.Entity<Models.Task>(task =>
           {
            
                task.ToTable("Task");
                task.HasKey(p => p.TaskId);
                task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
                task.Property(p => p.Title).IsRequired().HasMaxLength(200);
                task.Property(p => p.Description).IsRequired(false);
                task.Property(p => p.PriorityTask);
                task.Property(p => p.CreationDate);
                task.Ignore(p => p.Resumen);
                task.HasData(taskInit);

           });

        }
        
    }
}