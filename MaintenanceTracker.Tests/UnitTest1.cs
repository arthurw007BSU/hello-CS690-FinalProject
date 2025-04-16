namespace MaintenanceTracker.Tests;

using System.Text.Json;
using System.IO;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //test test :D
    }

    [Fact]
    //see if a task can be created
    public void MakeTask()
    {
        // setup task
        var task = new MaintenanceTask
        {
            Id = 1,
            Description = "Change air filter",
            DueDate = new DateTime(2025, 5, 1),
            IsCompleted = false
        };

        // Assert
        Assert.Equal(1, task.Id);
        Assert.Equal("Change air filter", task.Description);
        Assert.Equal(new DateTime(2025, 5, 1), task.DueDate);
        Assert.False(task.IsCompleted);
    }

    [Fact]
    //see if i can create an expense
    public void MakeExpense()
    {
        // setup expense
        int taskId = 1;
        decimal amount = 150.75m;
        DateTime date = new DateTime(2025, 5, 10);
        string notes = "Paid plumber for sink repair";

        // create the object
        Expense expense = new Expense
        {
            TaskId = taskId,
            Amount = amount,
            Date = date,
            Notes = notes
        };

        // Assert
        Assert.Equal(taskId, expense.TaskId);
        Assert.Equal(amount, expense.Amount);
        Assert.Equal(date, expense.Date);
        Assert.Equal(notes, expense.Notes);
    }
    [Fact]
    //test saving to a file
    public void SaveAndLoadExpensesFromFile()
    {
        // setup list to hold expense
        var expenses = new List<Expense>
        {
            //setup a new expense object
            new Expense
            {
                TaskId = 1,
                Amount = 120.00m,
                Date = new DateTime(2025, 4, 10),
                Notes = "Annual HVAC service"
            },
            new Expense
            {
                TaskId = 2,
                Amount = 80.00m,
                Date = new DateTime(2025, 4, 12),
                Notes = "Gutter cleaning"
            }
        };
        //test file for testing
        string filePath = "test_expenses.json";

        // Act: Save to file
        string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);

        // Act: Load from file
        string loadedJson = File.ReadAllText(filePath);
        var loadedExpenses = JsonSerializer.Deserialize<List<Expense>>(loadedJson);

        // Assert
        Assert.NotNull(loadedExpenses);
        Assert.Equal(expenses.Count, loadedExpenses.Count);

        for (int i = 0; i < expenses.Count; i++)
        {
            Assert.Equal(expenses[i].TaskId, loadedExpenses[i].TaskId);
            Assert.Equal(expenses[i].Amount, loadedExpenses[i].Amount);
            Assert.Equal(expenses[i].Date, loadedExpenses[i].Date);
            Assert.Equal(expenses[i].Notes, loadedExpenses[i].Notes);
        }

        // Cleanup
        // remove files from testing
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
//
    [Fact]
    //test to save and load task data
    public void SaveAndLoadTasksFromFile()
    {
        // Arrange
        //create the task list for task objects
        var tasks = new List<MaintenanceTask>
        {
            //create new objects
            new MaintenanceTask
            {
                Id = 1,
                Description = "Change air filter",
                DueDate = new DateTime(2025, 5, 1),
                IsCompleted = false,
                IsRecurring = true,
                RecurrenceDays = 90
            },
            new MaintenanceTask
            {
                Id = 2,
                Description = "Clean gutters",
                DueDate = new DateTime(2025, 6, 1),
                IsCompleted = true,
                IsRecurring = false,
                RecurrenceDays = 0
            }
        };

        string filePath = "test_tasks.json";

        // Act: Save to file
        //write to file in json format
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);

        // Act: Load from file
        string loadedJson = File.ReadAllText(filePath);
        var loadedTasks = JsonSerializer.Deserialize<List<MaintenanceTask>>(loadedJson);

        // Assert
        Assert.NotNull(loadedTasks);
        Assert.Equal(tasks.Count, loadedTasks.Count);

        for (int i = 0; i < tasks.Count; i++)
        {
            Assert.Equal(tasks[i].Id, loadedTasks[i].Id);
            Assert.Equal(tasks[i].Description, loadedTasks[i].Description);
            Assert.Equal(tasks[i].DueDate, loadedTasks[i].DueDate);
            Assert.Equal(tasks[i].IsCompleted, loadedTasks[i].IsCompleted);
            Assert.Equal(tasks[i].IsRecurring, loadedTasks[i].IsRecurring);
            Assert.Equal(tasks[i].RecurrenceDays, loadedTasks[i].RecurrenceDays);
        }

        // Cleanup
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    [Fact]
    //test finding recurring tasks
    public void FindRecurringTasks()
    {
        // Arrange
        //create a list for tasks objects
        TaskManager.Tasks = new List<MaintenanceTask>
        {
            //create tasks objects
            new MaintenanceTask
            {
                Id = 1,
                Description = "Test recurring (due soon)",
                DueDate = DateTime.Today.AddDays(3),
                IsRecurring = true,
                IsCompleted = false,
                RecurrenceDays = 30
            },
            new MaintenanceTask
            {
                Id = 2,
                Description = "Test recurring (not due soon)",
                DueDate = DateTime.Today.AddDays(10),
                IsRecurring = true,
                IsCompleted = false,
                RecurrenceDays = 30
            },
            new MaintenanceTask
            {
                Id = 3,
                Description = "Test non-recurring",
                DueDate = DateTime.Today.AddDays(3),
                IsRecurring = false,
                IsCompleted = false
            },
            new MaintenanceTask
            {
                Id = 4,
                Description = "Test recurring completed",
                DueDate = DateTime.Today.AddDays(2),
                IsRecurring = true,
                IsCompleted = true
            }
        };

        // Act
        // check if the task due date is within the 7 days
        var reminders = TaskManager.GetUpcomingRecurringTasks(7);

        // Assert
        Assert.Equal(1, reminders.Count);
        Assert.Equal(1, reminders[0].Id);
        Assert.Equal("Test recurring (due soon)", reminders[0].Description);
    }

}
