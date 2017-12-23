using System.Collections.Generic;
using DZ3a;

namespace VebAplikacijaJedan.Models
{
    public class CompletedViewModel
    {
        public List<TodoItem> _completedTodoItemList;

        public CompletedViewModel(List<TodoItem> completedTodoItemList)
        {
            _completedTodoItemList = completedTodoItemList;
        }
    }
}