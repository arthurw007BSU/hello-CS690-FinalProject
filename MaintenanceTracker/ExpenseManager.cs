namespace MaintenanceTracker
{
    //this class was built to handle the logic of storing, logging, and getting expenses from the user
    public static class ExpenseManager
    {
        //a list to store the expense objects in 
        public static List<Expense> Expenses = new List<Expense>();

        //this method gets the associated taskID from the user to link this expense to.
        //once the information is gathered from  the user it is added to the list and then stored to a file called expense.json for later
        public static void LogExpense()
        {
            Console.Clear();
            Console.WriteLine("-- Log an Expense --");

            //check the tasks list to see there are any
            if (TaskManager.Tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks to log this expense to.");
                Console.WriteLine("Press Enter to return to the main menu.");
                Console.ReadLine();
                return;
            }
            //if there are tasks, let the user select the taskid to associate this expense.
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

            //create the expense object with the user data
            var newExpense = new Expense
            {
                TaskId = taskId,
                Amount = amount,
                Date = DateTime.Now,
                Notes = notes
            };

            //add the expense to a list of expenses
            Expenses.Add(newExpense);
            //write the expense to a file.
            FileManager.SaveToFile("expenses.json", Expenses);

            Console.WriteLine("Expense logged successfully!");
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
        }
    }

}