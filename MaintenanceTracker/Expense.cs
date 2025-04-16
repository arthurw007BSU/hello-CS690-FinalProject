
using System;
namespace MaintenanceTracker{

// Each expense is linked to a specific task (by TaskId) and contains details about the cost, date, and description of the expense.

    public class Expense{
        public int TaskId { get; set; }  //associate the taskid with this expense
        public decimal Amount { get; set; }  //get the ammount from the user.
        public DateTime Date { get; set; }   //set the date for logging purposes
        public string Notes { get; set; }   //add notes to the expense
    }
}