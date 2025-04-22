// Created by: [Arthur]
/*
This class represents an expense related to a maintenance task.
It includes properties for the task ID, amount, date of the expense, and any notes.
*/
using System;

namespace MaintenanceTracker
{
    public class Expense
    {
        public int TaskId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}