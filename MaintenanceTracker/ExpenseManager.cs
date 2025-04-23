// Created by: [Arthur]
/*
This class manages all of the helper methods for the expenses tasks. 
including logging new expenses, and viewing logged expenses.
It also handles the association of expenses with tasks.
*/
 using Spectre.Console;

namespace MaintenanceTracker
{
    public static class ExpenseManager
    {
        public static List<Expense> Expenses = new();
        // gather information from the user to create a new expense object
        // to be added to the expense list.
        public static void LogExpense()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold yellow]-- Log an Expense --[/]");

            if (TaskManager.Tasks.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]There are no tasks to log this expense to.[/]");
                AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu.");
                Console.ReadLine();
                return;
            }

            AnsiConsole.MarkupLine("Select the task to associate the expense with:");
            foreach (var task in TaskManager.Tasks)
            {
                Console.WriteLine($"ID: {task.Id} | {task.Description}");
            }

            int taskId;
            while (true)
            {
                AnsiConsole.Markup("Enter the [yellow]task ID[/] (or type [green]'q'[/] to cancel): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "q")
                {
                    AnsiConsole.MarkupLine("Returning to the [green]main menu[/]...");
                    Console.ReadLine();
                    return;
                }

                if (int.TryParse(input, out taskId) && TaskManager.Tasks.Any(t => t.Id == taskId))
                {
                    break;
                }

                AnsiConsole.MarkupLine("[red]Invalid task ID. Try again or type 'q' to cancel.[/]");
            }

            decimal amount;
            while (true)
            {
                AnsiConsole.Markup("Enter [yellow]expense amount[/] (e.g., 125.50) or type [green]'q'[/] to cancel: ");
                string amountInput = Console.ReadLine()?.Trim().ToLower();

                if (amountInput == "q")
                {
                    AnsiConsole.MarkupLine("Returning to the [green]main menu[/]...");
                    Console.ReadLine();
                    return;
                }

                if (decimal.TryParse(amountInput, out amount) && amount > 0)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[red]Invalid amount. Please enter a positive number.[/]");
            }

            AnsiConsole.Markup("Enter a [yellow]note[/] for this expense (or leave blank, press Enter): ");
            string notes = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(notes))
            {
                notes = "(no notes)";
            }

            var newExpense = new Expense
            {
                TaskId = taskId,
                Amount = amount,
                Date = DateTime.Now,
                Notes = notes
            };

            Expenses.Add(newExpense);
            FileManager.SaveToFile("expenses.json", Expenses);

            AnsiConsole.MarkupLine("[green]Expense logged successfully![/]");
            AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu.");
            Console.ReadLine();
        }
    }
}