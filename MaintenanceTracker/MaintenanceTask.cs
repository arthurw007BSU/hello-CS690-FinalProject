
using System;
namespace MaintenanceTracker{


    public class MaintenanceTask{    
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

}

