using Microsoft.EntityFrameworkCore;
using projectEF.Models;

namespace projectEF.Contexts
{
    public class TaskContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public TaskContext (DbContextOptions<TaskContext> options) :base(options){}
        
    }
}