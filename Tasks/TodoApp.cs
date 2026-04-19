using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Linq;
using TodoApp.Classes;


namespace TodoApp.Tasks
{
    public class TodoManager
    {
        public static string FilePath = "Data/todos.json";
        public static List<TodoItem> todos = new List<TodoItem>();

        public static void Run() 
        {
            load();
            while (true)
            {
                showMenu();
                Console.Write("Choise: ");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    default: Console.WriteLine("Wrong options!"); break;
                    case "1": showAll(); break;
                    case "2": addTask(); break;
                    case "3": completeTask(); break;
                    case "4": deleteTask(); break;
                    case "5": save(); break;
                    case "0": Console.WriteLine("Bye bye"); return;
                }
            }
        }
        public static void showMenu()
        {
            Console.WriteLine("Welcome to ToDo App");
            Console.WriteLine("1. Show all tasks");
            Console.WriteLine("2. Add task");
            Console.WriteLine("3. Complete task");
            Console.WriteLine("4. Delete task");
            Console.WriteLine("5. Save");
            Console.WriteLine("0. Exit");
        }

        public static List<TodoItem> getSorted()
        {
            return todos.OrderBy(t => t.IsCompleted).ToList();
        }

        public static void showAll()
        {
            if (todos.Count == 0) 
            { 
                Console.WriteLine("No tasks!"); return; 
            }

            var sorted = getSorted(); // ← використовуємо getSorted
            for (int i = 0; i < sorted.Count; i++)
            {
                var task = sorted[i];
                string status = task.IsCompleted ? "[✓]" : "[ ]";
                Console.WriteLine($"{i + 1}. {status} | {task.TaskPriority} | {task.Title} | Deadline: {task.Deadline:dd.MM.yyyy}");
            }
        }
        public static void addTask()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Priority. 1 - High, 2 - Medium, 3 - Low");
            string priorityChoice = Console.ReadLine();
            TodoItem.Priority priority = priorityChoice switch
            {
                "1" => TodoItem.Priority.High,
                "2" => TodoItem.Priority.Medium,
                "3" => TodoItem.Priority.Low,
                _ => TodoItem.Priority.Medium
            };
            DateTime deadline = askDeadline();

            todos.Add(new TodoItem(title, deadline, priority));
            Console.WriteLine("Task added!");
        }
        public static DateTime askDeadline()
        {
            while (true)
            {
                Console.Write("Deadline (dd.MM.yyyy): ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime deadline))
                {
                    return deadline;
                }
                else
                {
                    Console.WriteLine("Wrong format! Try again (dd.MM.yyyy)");
                }
            }
        }
        public static void completeTask() 
        {
            var sorted = getSorted(); // ← той самий порядок
            showAll();
            var notCompleted = sorted.Where(t => !t.IsCompleted).ToList();

            if (notCompleted.Count == 0)
            {
                Console.WriteLine("No tasks to complete!");
                return;
            }

            while (true)
            {
                Console.Write("Enter task number: ");
                if (!int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    Console.WriteLine("Enter a number!");
                    continue;
                }
                if (taskNumber < 1 || taskNumber > sorted.Count)
                {
                    Console.WriteLine("Wrong number!");
                    continue;
                }

                // І перевіряй чи задача вже виконана
                if (sorted[taskNumber - 1].IsCompleted)
                {
                    Console.WriteLine("Task already completed!");
                    continue;
                }

                sorted[taskNumber - 1].IsCompleted = true;
                Console.WriteLine("Task completed! ✅");
                return;
            }
        }
        public static void deleteTask()
        {
            var sorted = getSorted();
            showAll();

            while (true)
            {
                Console.Write("Enter task number: ");
                if (!int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    Console.WriteLine("Enter a number!");
                    continue;
                }
                if (taskNumber < 1 || taskNumber > todos.Count)
                {
                    Console.WriteLine("Wrong number!");
                    continue;
                }

                todos.Remove(sorted[taskNumber - 1]); // видаляємо об'єкт
                Console.WriteLine("Task deleted!");
                return;
            }
        }
        public static void save()
        {
            try
            {
                Directory.CreateDirectory("Data");
                List<TodoData> dataList = todos
                    .Select(t => new TodoData
                    {
                        Title = t.Title,
                        IsCompleted = t.IsCompleted,
                        Deadline = t.Deadline,
                        CreatedAt = t.CreatedAt,
                        TaskPriority = t.TaskPriority.ToString(),
                        // допиши решту властивостей
                    }).ToList();

                string json = JsonSerializer.Serialize(dataList);
                File.WriteAllText(FilePath, json);
                Console.WriteLine("Saved! ✅");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void load()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return; // файлу нема — нічого не робимо

                string json = File.ReadAllText(FilePath);
                List<TodoData> dataList = JsonSerializer.Deserialize<List<TodoData>>(json);

                todos = dataList.Select(d => new TodoItem(
                    d.Title,
                    d.Deadline,
                    Enum.Parse<TodoItem.Priority>(d.TaskPriority) // string → enum
                )
                {
                    IsCompleted = d.IsCompleted,  // ці властивості
                    CreatedAt = d.CreatedAt       // встановлюємо окремо
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
