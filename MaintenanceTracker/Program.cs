namespace MaintenanceTracker;

using System.Text.Json;
using System.IO;

class Program
{
    //create a list of maintenances tasks objects for referencing and later writing to file.
    static List<MaintenanceTask> tasks = new List<MaintenanceTask>();
    static List<Expense> expenses = new List<Expense>();
    //Create a counter to keep up with the number of tasks
    static int taskCounter = 1;

    static string taskFilePath = "tasks.json";
    static string expenseFilePath = "expenses.json";
    static void Main(string[] args)
    {
        LoadTasksFromFile();
        LoadExpensesFromFile();

        //set the exit variable so the menu will keep going until the user decides to quit.
        bool exit = false;
        //while loop to keep displaying the menu
        while (!exit){
            Console.Clear();
            Console.WriteLine("=== Home Maintenance Tracker ===");
            Console.WriteLine("1. Log a Maintenance Task");
            Console.WriteLine("2. Mark a Task as Complete");
            Console.WriteLine("3. View Upcoming Tasks");
            Console.WriteLine("4. Log an Expense");
            Console.WriteLine("5. View reports");
            Console.WriteLine("6. Quit");
            Console.Write("Select an option (1-6): ");

            //get input from the user   
            string input = Console.ReadLine();
            //on input call the specified method
            switch (input){
                case "1":
                    LogTask(); //call the logtask method
                    break;
                case "2":
                    MarkTaskComplete();
                    break;
                case "3":
                    ViewUpcomingTasks();
                    break;
                case "4":
                    LogExpense();
                    break;
                case "5":
                    View_Reports();
                    break;
                case "6":
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Not an option, try again.");
                    Console.ReadLine();
                    break;
            }
        }

    }
    static void LogTask(){
        //clear the screen to make it cleaner for the user to see the menu

        Console.Clear();
        //give the new screen a title
        Console.WriteLine("-- Log a New Maintenance Task --");
        //get user input for the task description
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        //get the user input for the due date
        Console.Write("Enter due date (yyyy-mm-dd): ");
        string taskDate = Console.ReadLine();

        //tricky learning date time class.  gave it a shot
        // create a duedate variable to hold the user date input of type datetime.
        //
        DateTime dueDate;
        //I want to make sure the user gives me a good date in the correct format
        //until the user does, it will coninue to ask
        while (!DateTime.TryParse(taskDate, out dueDate)){
                Console.Write("Invalid date. Please enter again (yyyy-mm-dd): ");
                taskDate = Console.ReadLine();
            }

        //now that i have a due date, i can create a maintenancetask object to store in my list
        MaintenanceTask newTask = new MaintenanceTask();
        /* Id = taskCounter++,
            Description = description,
            DueDate = dueDate
            };
        */
        newTask.Id = taskCounter++;
        newTask.Description = description;
        newTask.DueDate = dueDate;


        //add the new object to my tasks list
        tasks.Add(newTask);
        SaveTasksToFile();
        Console.WriteLine("Task logged successfully!");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

    }

    static void MarkTaskComplete(){
        Console.WriteLine("\n-- Mark Task Complete --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
       
    }

    //print to screen the list of dates stored in my tasks list
    static void ViewUpcomingTasks(){
        /*
        Console.WriteLine("\n-- Log a Task --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
        */
        
        Console.Clear();
        Console.WriteLine("-- Upcoming Maintenance Tasks --");
        if (tasks.Count == 0){
            Console.WriteLine("No tasks have been logged.");
        }
        else{
            foreach (var task in tasks){
                string status = task.IsCompleted ? "Completed" : "Pending";
                Console.WriteLine($"ID: {task.Id} | {task.Description} | Due: {task.DueDate.ToShortDateString()} | {status}");
            }
        }

        Console.WriteLine("\nPress Enter to return to the main menu...");
        Console.ReadLine();
       
    }

    /*
    static void LogExpense(){
        Console.WriteLine("\n-- Log a Task --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
       
    }*/
    static void LogExpense(){
        Console.Clear();
        Console.WriteLine("-- Log an Expense --");

        if (tasks.Count == 0){
            Console.WriteLine("There are no tasks to log this expense to.");
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
            return;
        }

        // Show tasks to pick from
        Console.WriteLine("Select the task to associate the expense with:");
        foreach (var task in tasks){
            Console.WriteLine($"ID: {task.Id} | {task.Description}");
        }

        Console.Write("Enter the task ID: ");
        string input = Console.ReadLine();
        int taskId;

        while (!int.TryParse(input, out taskId) || !tasks.Any(t => t.Id == taskId)){
        Console.Write("Invalid task ID. Try again: ");
        input = Console.ReadLine();
        }

        Console.Write("Enter expense amount (e.g., 125.50): ");
        string amountInput = Console.ReadLine();
        decimal amount;
    
        while (!decimal.TryParse(amountInput, out amount)){
            Console.Write("Invalid amount. Try again: ");
            amountInput = Console.ReadLine();
        }

        Console.Write("Enter a note for this expense: ");
        string notes = Console.ReadLine();

        /*Expense newExpense = new Expense{
        TaskId = taskId,
        Amount = amount,
        Date = DateTime.Now,
        Notes = notes
        };
        */

        Expense newExpense = new Expense();
        newExpense.TaskId = taskId;
        newExpense.Amount = amount;
        newExpense.Date = DateTime.Now;
        newExpense.Notes = notes;

        // Add the new expense to the list of expenses
        expenses.Add(newExpense);
        SaveExpensesToFile();
        Console.WriteLine("Expense logged successfully!");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();

    }
    static void  View_Reports(){
        Console.WriteLine("\n-- Log a Task --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    static void SaveTasksToFile(){
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(taskFilePath, json);
    }

    static void SaveExpensesToFile(){
        string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(expenseFilePath, json);
        Console.WriteLine("Expenses saved to file.", json);

    }

    static void LoadTasksFromFile(){
        if (File.Exists(taskFilePath)){
            string json = File.ReadAllText(taskFilePath);
            tasks = JsonSerializer.Deserialize<List<MaintenanceTask>>(json) ?? new List<MaintenanceTask>();
        }
    }

    static void LoadExpensesFromFile(){
        if (File.Exists(expenseFilePath)){
            string json = File.ReadAllText(expenseFilePath);
            expenses = JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
        }
    }

}
