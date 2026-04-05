using System;
using System.Collections.Generic;
using System.Text;

using TodoApp.Classes;

namespace TodoApp.Tasks
{
    public class TodoManager
    {
        public static string FilePath = "Data/todos.json";
        public static List<TodoItem> todos = new List<TodoItem>();

        public static void Run() 
        {
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
        public static void showAll()
        {

        }
        public static void addTask()
        {

        }
        public static void completeTask() 
        {

        }
        public static void deleteTask()
        { 

        }
        public static void save()
        {

        }
        public static void load()
        {

        }
    }
}
