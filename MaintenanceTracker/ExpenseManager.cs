// Created by: [Arthur]
/*
This class manages all of the helper methods for the expenses tasks. 
including logging new expenses, and viewing logged expenses.
It also handles the association of expenses with tasks.
*/

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
            Console.WriteLine("-- Log an Expense --");

            if (TaskManager.Tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks to log this expense to.");
                Console.WriteLine("Press Enter to return to the main menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Select the task to associate the expense with:");

            foreach (var task in TaskManager.Tasks)
            {
                Console.WriteLine($"ID: {task.Id} | {task.Description}");
            }

            Console.Write("Enter the task ID: ");
            string input = Console.ReadLine();
            int taskId;

            while (!int.TryParse(input, out taskId) || !TaskManager.Tasks.Any(t => t.Id == taskId))
            {
                Console.Write("Invalid task ID. Try again: ");
                input = Console.ReadLine();
            }

            Console.Write("Enter expense amount (e.g., 125.50): ");
            string amountInput = Console.ReadLine();
            decimal amount;

            while (!decimal.TryParse(amountInput, out amount))
            {
                Console.Write("Invalid amount. Try again: ");
                amountInput = Console.ReadLine();
            }

            Console.Write("Enter a note for this expense: ");
            string notes = Console.ReadLine();

            var newExpense = new Expense
            {
                TaskId = taskId,
                Amount = amount,
                Date = DateTime.Now,
                Notes = notes
            };

            Expenses.Add(newExpense);
            FileManager.SaveToFile("expenses.json", Expenses);

            Console.WriteLine("Expense logged successfully!");
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
        }
    }
}