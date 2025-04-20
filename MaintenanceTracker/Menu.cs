using System;
//using MaintenanceTracker;
using Spectre.Console;

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
                AnsiConsole.Write(
                    new FigletText("Home Maintenance Tracker").Centered().Color(Color.Orange1));

                var input = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Select an option:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "🛠️  1. Log a Maintenance Task",
                        "✅  2. Mark a Task as Complete",
                        "📅  3. View Upcoming Tasks",
                        "💰  4. Log an Expense",
                        "⏰  5. View Reminders",
                        "🚪  6. Quit"
                    }));

        




                switch (input)
                {
                    case "🛠️  1. Log a Maintenance Task":
                        TaskManager.LogTask("tasks.json");
                        break;

                    case "✅  2. Mark a Task as Complete":
                        TaskManager.MarkTaskComplete("tasks.json");
                        break;

                    case "📅  3. View Upcoming Tasks":
                        TaskManager.ViewUpcomingTasks();
                        break;

                    case "💰  4. Log an Expense":
                        ExpenseManager.LogExpense();
                        break;

                    case "⏰  5. View Reminders":
                        TaskManager.ShowUpcomingRecurringReminders();
                        break;

                    case "🚪  6. Quit":
                        exit = true;
                        AnsiConsole.MarkupLine("[red]Goodbye![/]");
                        break;
                }

            }
        }
    }
}