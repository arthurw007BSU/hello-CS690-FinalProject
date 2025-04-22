// Created by: [Arthur]
/*
This class manages all of the helper methods for the maintenance tasks. 
including viewing upcoming tasks, logging new tasks, marking tasks as complete, and showing upcoming recurring reminders.
*/

namespace MaintenanceTracker
{
    public static class TaskManager
    {
        public static List<MaintenanceTask> Tasks = new();
        public static int TaskCounter = 1;

        public static void ViewUpcomingTasks()
        {
            Console.Clear();
            Console.WriteLine("-- Upcoming Maintenance Tasks --");

            if (Tasks.Count == 0)
            {
                Console.WriteLine("No tasks have been logged.");
            }
            else
            {
                var sortedTasks = Tasks.OrderBy(t => t.DueDate).ToList();

                foreach (var task in sortedTasks)
                {
                    string status = task.IsCompleted ? "Completed" : "Pending";
                    string recurrence = task.IsRecurring ? $" (Repeats every {task.RecurrenceDays} days)" : "";

                    Console.WriteLine($"ID: {task.Id} | {task.Description} | Due: {task.DueDate.ToShortDateString()} | {status}{recurrence}");
                }
            }

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        }

        public static void LogTask(string taskFilePath)
        {
            Console.Clear();
            Console.WriteLine("-- Log a New Maintenance Task --");

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter due date (yyyy-mm-dd): ");
            string taskDate = Console.ReadLine();

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
                    Console.Write("Invalid number. Please enter a positive number of days: ");
                    daysInput = Console.ReadLine();
                }
            }

            DateTime dueDate;
            while (!DateTime.TryParse(taskDate, out dueDate))
            {
                Console.Write("Invalid date. Please enter again (yyyy-mm-dd): ");
                taskDate = Console.ReadLine();
            }

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

            Console.WriteLine("Task logged successfully!");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        public static void MarkTaskComplete(string taskFilePath)
        {
            Console.Clear();
            Console.WriteLine("-- Mark Task as Complete --");

            if (Tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                Console.WriteLine("Press Enter to return to the main menu.");
                Console.ReadLine();
                return;
            }

            foreach (var task in Tasks.Where(t => !t.IsCompleted))
            {
                Console.WriteLine($"ID: {task.Id} | {task.Description} | Due: {task.DueDate.ToShortDateString()} | Pending");
            }

            Console.Write("Enter the ID of the task to mark as complete: ");
            string input = Console.ReadLine();
            int taskId;

            while (!int.TryParse(input, out taskId) || !Tasks.Any(t => t.Id == taskId))
            {
                Console.Write("Invalid ID. Try again: ");
                input = Console.ReadLine();
            }

            var selectedTask = Tasks.First(t => t.Id == taskId);
            selectedTask.IsCompleted = true;

            FileManager.SaveToFile(taskFilePath, Tasks);

            Console.WriteLine("Task marked as complete!");
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
        }

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
            }
            else
            {
                foreach (var task in upcoming)
                {
                    Console.WriteLine($"[!] {task.Description} | Due: {task.DueDate.ToShortDateString()}");
                }
            }

            Console.WriteLine("\nPress Enter to return to the main menu.");
            Console.ReadLine();
        }

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