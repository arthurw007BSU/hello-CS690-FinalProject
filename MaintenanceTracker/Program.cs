namespace MaintenanceTracker;

class Program
{
    //Create class for maintenance tasks to be removed from this main program later
    public class MaintenanceTask{
        //set variables for the instance
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
    //create a list of maintenances tasks objects for referencing and later writing to file.
    static List<MaintenanceTask> tasks = new List<MaintenanceTask>();
    //Create a counter to keep up with the number of tasks
    static int taskCounter = 1;
    static void Main(string[] args)
    {

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
            string? input = Console.ReadLine();
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
        /*Console.WriteLine("\n-- Log a Task --");
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

        //tricky learning date time class.  gave it a shot
        // create a duedate variable to hold the user date input of type datetime.
        //
        DateTime dueDate;
        //I want to make sure the user gives me a good date in the correct format
        //until the user does, it will coninue to ask
        while (!DateTime.TryParse(inputDate, out dueDate)){
                Console.Write("Invalid date. Please enter again (yyyy-mm-dd): ");
                inputDate = Console.ReadLine();
            }

        //now that i have a due date, i can create a maintenancetask object to store in my list
        MaintenanceTask newTask = new MaintenanceTask{
            Id = taskCounter++,
            Description = description,
            DueDate = dueDate
            };
        //add the new object to my tasks list
        tasks.Add(newTask);
        Console.WriteLine("Task logged successfully!");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

    }

    static void MarkTaskComplete(){
        Console.WriteLine("\n-- Log a Task --");
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

    static void LogExpense(){
        Console.WriteLine("\n-- Log a Task --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
       
    }
    static void  View_Reports(){
        Console.WriteLine("\n-- Log a Task --");
        Console.WriteLine("\n--NOT IMPLEMENTED YET --");
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

}
