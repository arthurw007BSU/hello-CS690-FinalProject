namespace MaintenanceTracker;

//class for an individual task creation
//will be removed from the main as i get a handle around this code
public class MaintenanceTask
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
}

//create a class for expense
//move out of this main file later
public class Expense
{
    public int TaskId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
}

class Program
{
    static List<MaintenanceTask> tasks = new List<MaintenanceTask>();
    static int taskCounter = 1;
    static void Main(string[] args)
    {
        bool exit = false;
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

            string? input = Console.ReadLine();

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

// Placeholder methods to define soon
    static void LogTask()
    {
        /*( Console.WriteLine("\n-- Log a Task --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
        */

        Console.Clear();
    Console.WriteLine("-- Log a New Maintenance Task --");

    Console.Write("Enter task description: ");
    string description = Console.ReadLine();

    Console.Write("Enter due date (yyyy-mm-dd): ");
    string inputDate = Console.ReadLine();

    DateTime dueDate;
    while (!DateTime.TryParse(inputDate, out dueDate))
    {
        Console.Write("Invalid date. Please enter again (yyyy-mm-dd): ");
        inputDate = Console.ReadLine();
    }

    MaintenanceTask newTask = new MaintenanceTask
    {
        Id = taskCounter++,
        Description = description,
        DueDate = dueDate
    };

    tasks.Add(newTask);
    Console.WriteLine("Task logged successfully!");
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
    }

    static void MarkTaskComplete()
    {
        Console.WriteLine("\n-- Mark a Task Complete --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    static void ViewUpcomingTasks()
    {
        /*
        Console.WriteLine("\n-- View Upcoming Tasks --");
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
                string status = task.IsCompleted ? "Completed" : " Pending";
                Console.WriteLine($"ID: {task.Id} | {task.Description} | Due: {task.DueDate.ToShortDateString()} | {status}");
            }
        }   

        Console.WriteLine("\nPress Enter to return to the main menu...");
        Console.ReadLine();
    }

    static void LogExpense()
    {
        Console.WriteLine("\n-- Log an Expense --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }
        static void View_Reports()
    {
        Console.WriteLine("\n-- got to reports menu --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }
    }
}
