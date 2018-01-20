
using System;
using DZ3a;

namespace Zadatak1Test
{
    class Program
    {
        private const string ConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=UniversityDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        static void Main(string[] args)
        {
            TodoSqlRepository repo = new TodoSqlRepository(new TodoDbContext(ConnectionString));
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            Guid id3 = Guid.NewGuid();
            TodoItem item1 = new TodoItem("item1", id1);
            TodoItem item2 = new TodoItem("item2", id2);
            TodoItem item3 = new TodoItem("item3", id3);
            TodoItem item4 = new TodoItem("item4", id1);
            TodoItem item5 = new TodoItem("item5", id2);
            TodoItem item6 = new TodoItem("item6", id3);
            repo.Add(item1);
            Console.WriteLine(repo.Get(item1.Id,id1).Text);
            Console.ReadLine();
        }
    }
}