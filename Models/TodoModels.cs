public class TodoItem
{
    public string Text { get; set; } = "";
    public bool IsDone { get; set; }
}

public class TodoList
{
    public string Name { get; set; } = "";
    public List<TodoItem> Items { get; set; } = new();
}