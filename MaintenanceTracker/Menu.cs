using System;
//using MaintenanceTracker;

namespace MaintenanceTracker
{
    public static class Menu
    {
        public static void ShowMainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Home Maintenance Tracker ===");
                Console.WriteLine("1. Log a Maintenance Task");
                Console.WriteLine("2. Mark a Task as Complete");
                Console.WriteLine("3. View Upcoming Tasks");
                Console.WriteLine("4. Log an Expense");
                Console.WriteLine("5. View Reminders");
                Console.WriteLine("6. Quit");
                Console.Write("Select an option (1-6): ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        TaskManager.LogTask("tasks.json");
                        break;

                    case "2":
                        TaskManager.MarkTaskComplete("tasks.json");
                        break;

                    case "3":
                        TaskManager.ViewUpcomingTasks();
                        break;

                    case "4":
                        ExpenseManager.LogExpense();
                        break;

                    case "5":
                        TaskManager.ShowUpcomingRecurringReminders();
                        break;

                    case "6":
                        exit = true;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid input. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}