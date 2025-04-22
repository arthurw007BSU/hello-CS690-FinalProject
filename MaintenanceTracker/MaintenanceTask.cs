// Created by: [Arthur]
/* This class defines the structure for a maintenance task.
   Each task has an ID, a description, a due date, and status flags
   for whether it is completed and whether it is a recurring task.
*/

using System;

namespace MaintenanceTracker
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsRecurring { get; set; } = false;
        public int RecurrenceDays { get; set; }
    }
}

