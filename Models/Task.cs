namespace projectEF.Models
{
    public class Task
    {
        public Guid TareaId { get; set; }
        public Guid CategoryId { get; set; }
        public string Titulo { get; set; }
        public string Description { get; set; }
        public Priority PriorityTask { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Category Category { get; set; }
    }
}