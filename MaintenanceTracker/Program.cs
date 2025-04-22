namespace MaintenanceTracker;

using System.Text.Json;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
    TaskManager.Tasks = FileManager.LoadFromFile<MaintenanceTask>("tasks.json");
    TaskManager.TaskCounter = TaskManager.Tasks.Count + 1;

    // Display upcoming recurring tasks at startup
    Console.Clear();
    

    // Display upcoming recurring tasks at startup
    TaskManager.ShowUpcomingRecurringReminders();

    ExpenseManager.Expenses = FileManager.LoadFromFile<Expense>("expenses.json");
    
    Menu.ShowMainMenu(); 

    }
    
}
