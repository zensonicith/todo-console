/*

    OOP: TodoItem class: properties (Title, Description, IsCompleted), methods (MarkAsCompleted, ToString)
    List<T>: contains TodoItem objects
    enum: TodoStatus: Todo, InProgress, Done
    LINQ + control flow: manage functionality based on user input: crud, mark complete, sort by status

 */

using TodoList;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Todo List Application!");
        ITodoOperators todoOperations = new TodoOperationsImpl();
        int nextId = 1;
        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. View all todos");
            Console.WriteLine("2. Add a new todo");
            Console.WriteLine("3. Update a todo");
            Console.WriteLine("4. Patch a todo");
            Console.WriteLine("5. Remove a todo");
            Console.WriteLine("6. Change status of a todo");
            Console.WriteLine("7. Sort by status");
            Console.WriteLine("8. Sort by completion");
            Console.WriteLine("9. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var todos = todoOperations.GetTodos();
                    foreach (var todo in todos)
                    {
                        Console.WriteLine($"ID: {todo.Id}, {todo}");
                    }
                    break;

                case "2":
                    Console.Write("Enter title: ");
                    string title = Console.ReadLine() ?? string.Empty;
                    Console.Write("Enter description: ");
                    string description = Console.ReadLine() ?? string.Empty;
                    var newTodo = new TodoItem(title, description) { Id = nextId++ };
                    todoOperations.AddNewTodo(newTodo);
                    Console.WriteLine("Todo added successfully.");
                    break;

                case "3":
                    Console.Write("Enter ID of the todo to update: ");
                    if (!int.TryParse(Console.ReadLine(), out int updateId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    var itemToUpdate = todoOperations.GetTodoById(updateId);
                    if (itemToUpdate == null)
                    {
                        Console.WriteLine("Todo not found.");
                        break;
                    }
                    Console.Write("Enter new title: ");
                    string newTitle = Console.ReadLine() ?? itemToUpdate.Title;
                    Console.Write("Enter new description: ");
                    string newDescription = Console.ReadLine() ?? itemToUpdate.Description;
                    todoOperations.UpdateTodoById(updateId, newTitle, newDescription);
                    Console.WriteLine("Todo updated successfully.");
                    break;

                case "4":
                    Console.Write("Enter ID of the todo to patch: ");
                    if (!int.TryParse(Console.ReadLine(), out int patchId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    var itemToPatch = todoOperations.GetTodoById(patchId);
                    if (itemToPatch == null)
                    {
                        Console.WriteLine("Todo not found.");
                        break;
                    }
                    Console.Write("Enter new title (leave blank to keep unchanged): ");
                    string patchTitle = Console.ReadLine();
                    Console.Write("Enter new description (leave blank to keep unchanged): ");
                    string patchDescription = Console.ReadLine();
                    todoOperations.PatchTodoById(patchId,
                        string.IsNullOrWhiteSpace(patchTitle) ? null : patchTitle,
                        string.IsNullOrWhiteSpace(patchDescription) ? null : patchDescription);
                    Console.WriteLine("Todo patched successfully.");
                    break;

                case "5":
                    Console.Write("Enter ID of the todo to remove: ");
                    if (!int.TryParse(Console.ReadLine(), out int removeId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    var itemToRemove = todoOperations.GetTodoById(removeId);
                    if (itemToRemove == null)
                    {
                        Console.WriteLine("Todo not found.");
                        break;
                    }
                    todoOperations.RemoveTodoById(removeId);
                    Console.WriteLine("Todo removed successfully.");
                    break;

                case "6":
                    Console.Write("Enter ID of the todo to change status: ");
                    if (!int.TryParse(Console.ReadLine(), out int statusId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }
                    var itemToChange = todoOperations.GetTodoById(statusId);
                    if (itemToChange == null)
                    {
                        Console.WriteLine("Todo not found.");
                        break;
                    }
                    Console.WriteLine("Select new status: 1. Todo 2. InProgress 3. Done");
                    string statusChoice = Console.ReadLine();
                    TodoStatus? newStatus = statusChoice switch
                    {
                        "1" => TodoStatus.Todo,
                        "2" => TodoStatus.InProgress,
                        "3" => TodoStatus.Done,
                        _ => null
                    };
                    if (newStatus == null)
                    {
                        Console.WriteLine("Invalid status choice.");
                        break;
                    }
                    todoOperations.ChangeStatusById(statusId, newStatus.Value);
                    Console.WriteLine("Todo status changed successfully.");
                    break;

                case "7":
                    Console.WriteLine("Select status to filter: 1. Todo 2. InProgress 3. Done");
                    string sortChoice = Console.ReadLine();
                    TodoStatus? chooseStatus = sortChoice switch
                    {
                        "1" => TodoStatus.Todo,
                        "2" => TodoStatus.InProgress,
                        "3" => TodoStatus.Done,
                        _ => null
                    };
                    if (chooseStatus == null)
                    {
                        Console.WriteLine("Invalid status choice.");
                        break;
                    }
                    Console.WriteLine($"{chooseStatus} tasks: ");
                    var sortItems = todoOperations.GetTodosByStatus(chooseStatus.Value);
                    foreach (var item in sortItems)
                    {
                        Console.WriteLine($"ID: {item.Id}, {item}");
                    }
                    break;

                case "8":
                    Console.WriteLine("Completed tasks:");
                    var completedItem = todoOperations.GetTodosByCompletion();
                    foreach (var item in completedItem)
                    {
                        Console.WriteLine($"ID: {item.Id}, {item}");
                    }
                    break;

                case "9":
                    Console.WriteLine("Exiting the application. Goodbye!");
                    return;
            }
        }
    }
}