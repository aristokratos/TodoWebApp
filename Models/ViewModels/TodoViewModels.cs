namespace AnotherTodo.Models.ViewModels
{
    public class TodoViewModels
    {
       
#pragma warning disable CS8618 // Non-nullable property 'TodoList' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
            public List<TodoItem> TodoList { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'TodoList' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Todo' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
            public TodoItem Todo { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Todo' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        
    }
}
