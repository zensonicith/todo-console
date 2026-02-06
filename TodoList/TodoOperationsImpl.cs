namespace TodoList
{
    internal class TodoOperationsImpl : ITodoOperators
    {
        private List<TodoItem> _todoItems = new List<TodoItem>();

        public List<TodoItem> GetTodos()
        {
            return _todoItems.OrderBy(t => t.Id).ToList();
        }

        public TodoItem? GetTodoById(int id)
        {
            return _todoItems.FirstOrDefault(t => t.Id == id);
        }

        public void AddNewTodo(TodoItem item)
        {
            _todoItems.Add(item);
        }

        public void UpdateTodoById(int id, string newtitle, string newDescription)
        {
            TodoItem? existedItem = GetTodoById(id);
            if (existedItem != null)
            {
                existedItem.Title = newtitle;
                existedItem.Description = newDescription;
            }
        }

        public void PatchTodoById(int id, string? newtitle, string? newDescription)
        {
            TodoItem existedItem = GetTodoById(id);
            if (existedItem != null)
            {
                if (!string.IsNullOrEmpty(newtitle))
                {
                    existedItem.Title = newtitle;
                }
                if (!string.IsNullOrEmpty(newDescription))
                {
                    existedItem.Description = newDescription;
                }
            }
        }

        public void RemoveTodoById(int id)
        {
            TodoItem? existedItem = GetTodoById(id);
            if (existedItem != null)
            {
                _todoItems.Remove(existedItem);
            }
        }

        public void ChangeStatusById(int id, TodoStatus newStatus)
        {
            TodoItem? existedItem = GetTodoById(id);
            if (existedItem == null) return;
            existedItem.Status = newStatus;
        }

        public List<TodoItem> GetTodosByStatus(TodoStatus status)
        {
            return _todoItems.Where(t => t.Status == status).ToList();
        }

        public List<TodoItem> GetTodosByCompletion()
        {
            return _todoItems.Where(t => t.Status == TodoStatus.Done).ToList();
        }
    }
}