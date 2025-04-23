// Created by: [Arthur]
/* This class displays the main menu to the user using Spectre.Console for fancy text stuff.
   It presents options to log tasks, mark tasks complete, view upcoming tasks, log expenses, view reminders, or quit.
   Based on the user's selection, Menu.cs calls the appropriate methods from TaskManager or ExpenseManager.
*/

using System;
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
               //Banner
                AnsiConsole.Write(
                    new FigletText("Home Maintenance Tracker")
                        .LeftJustified()
                        .Color(Color.Orange1));
                        
                //user selection
                var input = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Select an option:[/]")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "🛠️  Log a Maintenance Task",
                            "✅  Mark a Task as Complete",
                            "📅  View Upcoming Tasks",
                            "💰  Log an Expense",
                            "⏰  View Reminders",
                            "🚪  Quit"
                        }));

                switch (input)
                {
                    case "🛠️  Log a Maintenance Task":
                        TaskManager.LogTask("tasks.json");
                        break;

                    case "✅  Mark a Task as Complete":
                        TaskManager.MarkTaskComplete("tasks.json");
                        break;

                    case "📅  View Upcoming Tasks":
                        TaskManager.ViewUpcomingTasks();
                        break;

                    case "💰  Log an Expense":
                        ExpenseManager.LogExpense();
                        break;

                    case "⏰  View Reminders":
                        TaskManager.ShowUpcomingRecurringReminders();
                        break;

                    case "🚪  Quit":
                        exit = true;
                        AnsiConsole.MarkupLine("[red]Goodbye![/]");
                        break;
                }
            }
        }
    }
}