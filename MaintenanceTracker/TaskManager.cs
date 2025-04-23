// Created by: [Arthur]
/*
This class manages all of the helper methods for the maintenance tasks. 
including viewing upcoming tasks, logging new tasks, marking tasks as complete, and showing upcoming recurring reminders.
*/

using Spectre.Console;

namespace MaintenanceTracker
{
    public static class TaskManager
    {
        public static List<MaintenanceTask> Tasks = new();
        public static int TaskCounter = 1;

        //Basically gather information form task list
        //if ithis within the date range, display it to the screen
        public static void ViewUpcomingTasks()
        {
            // Display upcoming tasks that are not completed
            // and are due today or later
            Console.Clear();
            Console.WriteLine("-- Upcoming Maintenance Tasks --");

            // Filter for upcoming and incomplete tasks
            var upcomingTasks = Tasks
                .Where(t => !t.IsCompleted && t.DueDate >= DateTime.Today)
                .OrderBy(t => t.DueDate)
                .ToList();

            if (upcomingTasks.Count == 0)
            {
                Console.WriteLine("No upcoming tasks.");
            }
            else
            {
                foreach (var task in upcomingTasks)
                {
                    string recurrence = task.IsRecurring ? $" (Repeats every {task.RecurrenceDays} days)" : "";
                    Console.WriteLine($"ID: {task.Id} | {task.Description} | Due: {task.DueDate.ToShortDateString()}{recurrence}");
                }
            }

            AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
            Console.ReadLine();

        }

        //Gather information from the user to create a new task object
        //to be added to the task list.
        public static void LogTask(string taskFilePath)
        {
            Console.Clear();
            Console.WriteLine("-- Log a New Maintenance Task --");

            // Require non-empty description
            string description;
            do
            {
                Console.Write("Enter task description: ");
                description = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(description))
                {
                    AnsiConsole.MarkupLine("[red]Description cannot be empty. Please enter a valid task description.[/]");
                    AnsiConsole.MarkupLine("[yellow]Example: Change air filter, clean gutters, service HVAC[/]");
                }

            } while (string.IsNullOrWhiteSpace(description));

            // Ask if the task is recurring
            Console.Write("Is this a recurring task? (y/n): ");
            string recurringInput = Console.ReadLine()?.ToLower();
            bool isRecurring = recurringInput == "y";
            int recurrenceDays = 0;

            if (isRecurring)
            {
                Console.Write("How many days between each recurrence (e.g., 90): ");
                string daysInput = Console.ReadLine();

                while (!int.TryParse(daysInput, out recurrenceDays) || recurrenceDays <= 0)
                {
                    AnsiConsole.Markup("[red]Invalid number.[/] [white]Please enter a[/] [yellow]positive number of days[/]: ");
                    daysInput = Console.ReadLine();
                }
            }

            // Require valid and non-past due date
            //DateTime dueDate;
            DateTime dueDate = DateTime.MinValue;
            bool isValidDate = false;

            while (!isValidDate)
            {
                Console.Write("Enter due date (yyyy-mm-dd): ");
                string taskDate = Console.ReadLine();

                if (!DateTime.TryParse(taskDate, out dueDate))
                {
                    AnsiConsole.MarkupLine("[red]Invalid date format. Please use yyyy-mm-dd.[/]");
                    continue;
                }

                if (dueDate < DateTime.Today)
                {
                    AnsiConsole.MarkupLine("[red]Due date cannot be in the past. Please enter a future date.[/]");
                    continue;
                }

                isValidDate = true;
            }

            // Create and save the task
            var newTask = new MaintenanceTask
            {
                Id = TaskCounter++,
                Description = description,
                DueDate = dueDate,
                IsRecurring = isRecurring,
                RecurrenceDays = recurrenceDays
            };

            Tasks.Add(newTask);
            FileManager.SaveToFile(taskFilePath, Tasks);

            AnsiConsole.MarkupLine("[green]Task logged successfully![/]");
            AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
            Console.ReadLine();
        }
        //change the value of the iscomleted field of the task object to true
        public static void MarkTaskComplete(string taskFilePath)
        {
            Console.Clear();
            Console.WriteLine("-- Mark Task as Complete --");

            var pendingTasks = Tasks
                .Where(t => !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToList();

            if (pendingTasks.Count == 0)
            {
                Console.WriteLine("No pending tasks available.");
                AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
                Console.ReadLine();

                return;
            }

            // Display pending tasks
            foreach (var task in pendingTasks)
            {
                Console.WriteLine($"ID: {task.Id} | {task.Description} | Due: {task.DueDate.ToShortDateString()} | Pending");
            }

            Console.Write("Enter the ID of the task to mark as complete (or type 'q' to cancel): ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "q")
            {
                AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
                Console.ReadLine();
                return;
            }

            int taskId;
            while (!int.TryParse(input, out taskId) || !pendingTasks.Any(t => t.Id == taskId))
            {
                AnsiConsole.Markup("[red]Invalid ID[/] [white]– Try again or type[/] [yellow]'q'[/] [white]to cancel: [/]");

                input = Console.ReadLine()?.Trim().ToLower();
                
                if (input == "q")
                {
                    AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
                    Console.ReadLine();
                    return;
                }
            }

            var selectedTask = Tasks.First(t => t.Id == taskId);
            selectedTask.IsCompleted = true;

            FileManager.SaveToFile(taskFilePath, Tasks);

            Console.WriteLine("Task marked as complete!");
            AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
            Console.ReadLine();

        }
        //this method will show the user all of the tasks that are due in the next 7 days
        //and are not completed. It will also show the user the date of the task.
        //if there are no tasks due, it will inform the user of that.   
        public static void ShowUpcomingRecurringReminders(int daysAhead = 7)
        {
            Console.Clear();
            Console.WriteLine($"-- Recurring Tasks Due in Next {daysAhead} Days --");

            DateTime now = DateTime.Today;
            DateTime threshold = now.AddDays(daysAhead);

            var upcoming = Tasks
                .Where(t => !t.IsCompleted && t.DueDate >= now && t.DueDate <= threshold)
                .OrderBy(t => t.DueDate)
                .ToList();

            if (upcoming.Count == 0)
            {
                Console.WriteLine("No recurring tasks are due soon.");
                AnsiConsole.MarkupLine("Press [green]Enter[/] to return to the main menu");
            }
            else
            {
                foreach (var task in upcoming)
                {
                    Console.WriteLine($"[!] {task.Description} | Due: {task.DueDate.ToShortDateString()}");
                }
            }

            Console.ReadLine();
        }
        //this method will return a list of all the tasks that are due in the next 7 days        
        public static List<MaintenanceTask> GetUpcomingRecurringTasks(int daysAhead = 7)
        {
            DateTime now = DateTime.Today;
            DateTime threshold = now.AddDays(daysAhead);

            return Tasks
                .Where(t => t.IsRecurring && !t.IsCompleted && t.DueDate >= now && t.DueDate <= threshold)
                .OrderBy(t => t.DueDate)
                .ToList();
        }
    }
}