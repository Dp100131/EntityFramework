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
            modelBuilder.Entity<Category>(category => 
            {
                
                category.ToTable("Category");
                category.HasKey(p => p.CategoryId);
                category.Property(p => p.Name).IsRequired().HasMaxLength(150);
                category.Property(p => p.Description);

            });

           modelBuilder.Entity<Models.Task>(task =>
           {
            
                task.ToTable("Task");
                task.HasKey(p => p.TareaId);
                task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
                task.Property(p => p.Title).IsRequired().HasMaxLength(200);
                task.Property(p => p.Description);
                task.Property(p => p.PriorityTask);
                task.Property(p => p.CreationDate);

           });

        }
        
    }
}