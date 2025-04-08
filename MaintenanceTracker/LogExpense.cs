    static void LogExpense()
    {
        Console.WriteLine("\n-- Log an Expense --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

Console.Clear();
    Console.WriteLine("-- Log an Expense --");

    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks available. Please add a task first.");
        Console.WriteLine("Press Enter to return...");
        Console.ReadLine();
        return;
    }

    // List tasks to pick from
    Console.WriteLine("Select the task to associate the expense with:");
    foreach (var task in tasks)
    {
        Console.WriteLine($"ID: {task.Id} | {task.Description}");
    }

    Console.Write("Enter task ID: ");
    string input = Console.ReadLine();
    int taskId;

    while (!int.TryParse(input, out taskId) || !tasks.Any(t => t.Id == taskId))
    {
        Console.Write("Invalid task ID. Enter again: ");
        input = Console.ReadLine();
    }

    Console.Write("Enter expense amount (e.g., 99.99): ");
    string amountInput = Console.ReadLine();
    decimal amount;
    while (!decimal.TryParse(amountInput, out amount))
    {
        Console.Write("Invalid amount. Enter again: ");
        amountInput = Console.ReadLine();
    }

    Console.Write("Enter notes or description for this expense: ");
    string notes = Console.ReadLine();

    Expense newExpense = new Expense
    {
        TaskId = taskId,
        Amount = amount,
        Date = DateTime.Now,
        Notes = notes
    };

    expenses.Add(newExpense);
    Console.WriteLine("Expense logged successfully!");
    Console.WriteLine("Press Enter to return...");
    Console.ReadLine();

}