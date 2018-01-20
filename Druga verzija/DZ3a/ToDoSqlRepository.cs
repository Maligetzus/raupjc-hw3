using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;

namespace DZ3a
{
    public class TodoSqlRepository : ITodoRepository
    {
        
        private readonly TodoDbContext _context;
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            if (!_context.TodoItems.FirstOrDefault(p => p.Id.Equals(todoId)).UserId.Equals(userId))
                throw new TodoAccessDeniedException("Ne smiješ");
            return _context.TodoItems.FirstOrDefault(p => p.Id.Equals(todoId));
        }

        public void Add(TodoItem todoItem)
        {
            if(_context.TodoItems.Any(t=>t.Id==todoItem.Id))
                throw new TodoDuplicateException(todoItem.Id.ToString());
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            if (!_context.TodoItems.Contains(Get(todoId, userId))) return false;
            if (!_context.TodoItems.FirstOrDefault(p => p.Id.Equals(todoId)).UserId.Equals(userId))
                throw new TodoAccessDeniedException("Ne smiješ");
            _context.TodoItems.Remove(this.Get(todoId,userId));
            _context.SaveChanges();
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (!_context.TodoItems.Contains(todoItem))
            {
                Add(todoItem);
                return;
            }
            if (!_context.TodoItems.FirstOrDefault(p => p.Id.Equals(todoItem.Id)).UserId.Equals(userId))
                throw new TodoAccessDeniedException("Nemoj");
            Remove(todoItem.Id,userId);
            Add(todoItem);
            _context.SaveChanges();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            if (!_context.TodoItems.FirstOrDefault(p => p.Id.Equals(todoId)).UserId.Equals(userId))
                throw new TodoAccessDeniedException("Ne smiješ");
            TodoItem todoItem=Get(todoId,userId);
            todoItem.MarkAsCompleted();
            _context.TodoItems.AddOrUpdate(todoItem);
            _context.SaveChanges();
            if (Get(todoId, userId).IsCompleted) return true;
            else return false;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(p => p.UserId.Equals(userId)).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItems.Where(p => !p.IsCompleted && p.UserId.Equals(userId)).ToList();

        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(p => p.IsCompleted && p.UserId.Equals(userId)).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.Where(p=>p.UserId.Equals(userId) && filterFunction(p)).ToList();
        }
    }
        
    public class TodoDuplicateException : Exception
    {
        private string _id;
        public TodoDuplicateException(string id)
        {
            _id = id;
            Console.WriteLine("duplicate id: " + id);
        }
    }

    public class TodoAccessDeniedException : Exception
    {
        private string _message;
        public TodoAccessDeniedException(string message)
        {
            _message = message;
            Console.WriteLine(message);
        }
    }
}
