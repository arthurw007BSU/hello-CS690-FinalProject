namespace MaintenanceTracker;

using System.Text.Json;
using System.IO;

class Program
{   
    static void Main(string[] args)
    {
        //load data from stored file
        TaskManager.Tasks = FileManager.LoadFromFile<MaintenanceTask>("tasks.json");
        //increment task count for setting taskID
        TaskManager.TaskCounter = TaskManager.Tasks.Count + 1;

        //load expense data from stored file
        ExpenseManager.Expenses = FileManager.LoadFromFile<Expense>("expenses.json");
        
        //start the menu
        Menu.ShowMainMenu(); 

    }
    
}
