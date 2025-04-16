
using System;
namespace MaintenanceTracker{


    public class MaintenanceTask{    
        public int Id { get; set; }  //place for id number
        public string Description { get; set; }//placeholder for description of task
        public DateTime DueDate { get; set; }   //record a date
        public bool IsCompleted { get; set; } = false;  //becaus it is a new task it is not complete
        public bool IsRecurring { get; set; } = false;  //the user will specify if it is recurring.  default is No

        public int RecurrenceDays { get; set; } = 0;  //set to 0 initially until the isrecurring attribute is set to TRUE

    

    }

}

