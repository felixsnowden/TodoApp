using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Classes
{
    public class TodoData
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TaskPriority { get; set; } = string.Empty;
    }
}
