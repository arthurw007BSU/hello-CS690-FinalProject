// Created by: [Arthur]
/* This is the main the Maintenance Tracker application.
   It loads saved tasks and expenses from files, displays upcoming reminders,
   and starts the main menu loop 
*/
namespace MaintenanceTracker;

using System.Text.Json;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        TaskManager.Tasks = FileManager.LoadFromFile<MaintenanceTask>("tasks.json");
        TaskManager.TaskCounter = TaskManager.Tasks.Count + 1;

        Console.Clear();

        TaskManager.ShowUpcomingRecurringReminders();

        ExpenseManager.Expenses = FileManager.LoadFromFile<Expense>("expenses.json");

        Menu.ShowMainMenu();
    }
}