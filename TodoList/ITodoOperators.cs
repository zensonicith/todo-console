namespace TodoList
{
    internal interface ITodoOperators
    {
        List<TodoItem> GetTodos();

        TodoItem GetTodoById(int id);

        void AddNewTodo(TodoItem item);

        void UpdateTodoById(int id, string newtitle, string newDescription);

        void PatchTodoById(int id, string? newtitle, string? newDescription);

        void RemoveTodoById(int id);

        void ChangeStatusById(int id, TodoStatus newStatus);

        List<TodoItem> GetTodosByStatus(TodoStatus status);

        List<TodoItem> GetTodosByCompletion();
    }
}