namespace TodoList
{
    internal class TodoItem(string title, string description)
    {
        // Properties
        public int Id { get; set; }

        public String Title { get; set; } = title;

        public String Description { get; set; } = description;

        public TodoStatus Status { get; set; } = TodoStatus.Todo;

        // Methods
        public override string ToString()
        {
            return $"Title: {Title}, Description: {Description}, Status: {Status}";
        }
    }
}