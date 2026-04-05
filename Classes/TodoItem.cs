using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Classes
{
    public class TodoItem
    {
        public enum Priority { Low, Medium, High }

        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public Priority TaskPriority { get; set; }

        public TodoItem(string title, DateTime deadline, Priority priority)
        {
            Title = title;
            Deadline = deadline;
            TaskPriority = priority;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
        }
    }
}
