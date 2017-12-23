using System.Data.Entity;

namespace DZ3a
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(string cnnstr) : base(cnnstr)
        {
        }
        public IDbSet<TodoItem> TodoItems { get; set; }
        public IDbSet<TodoItemLabel> ToDoItemLabels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(t=>t.Id);
            modelBuilder.Entity<TodoItem>().Property(t=>t.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t=>t.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().HasMany(t=>t.Labels).WithMany(l=>l.LabelTodoItems);
            modelBuilder.Entity<TodoItemLabel>().HasKey(l => l.Id);
            modelBuilder.Entity<TodoItemLabel>().Property(l => l.Value).IsRequired();
            modelBuilder.Entity<TodoItemLabel>().HasMany(l => l.LabelTodoItems).WithMany(t=>t.Labels);
        }
    }
}
