using System;

namespace TaskManager.Model
{
    public class TaskItem
    {
        public TaskItem()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Deadline = DateTime.Now;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Deadline { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
    }
}
