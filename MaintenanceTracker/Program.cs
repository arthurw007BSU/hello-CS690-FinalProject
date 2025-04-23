// Created by: [Arthur]
/* This is the main the Maintenance Tracker application.
   It loads saved tasks and expenses from files, displays upcoming reminders,
   and starts the main menu loop 
*/

using System;
using System.Text;

namespace MaintenanceTracker;

using System.Text.Json;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Set console to UTF-8 to allow emojis
        Console.OutputEncoding = Encoding.UTF8;
        
        //load tasks and expenses from files
        TaskManager.Tasks = FileManager.LoadFromFile<MaintenanceTask>("tasks.json");
        ExpenseManager.Expenses = FileManager.LoadFromFile<Expense>("expenses.json");
        TaskManager.TaskCounter = TaskManager.Tasks.Count + 1;

        // Show upcoming recurring reminders
        Console.Clear();
        TaskManager.ShowUpcomingRecurringReminders();

        Menu.ShowMainMenu();
    }
}