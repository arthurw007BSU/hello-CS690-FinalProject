
using System;
namespace MaintenanceTracker{

    public class Expense{
        public int TaskId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}