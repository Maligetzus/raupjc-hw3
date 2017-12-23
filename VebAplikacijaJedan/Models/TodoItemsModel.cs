using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DZ3a;

namespace VebAplikacijaJedan.Models
{
    public class TodoItemsModel
    {
        public List<TodoItem> _todoItemsList;

        public TodoItemsModel(List<TodoItem> todoItemsList)
        {
            _todoItemsList = todoItemsList;
        }
    }
}
