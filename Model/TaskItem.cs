using System;
using System.ComponentModel;

namespace TaskManager.Model
{
    public class TaskItem:INotifyPropertyChanged
    {
        public TaskItem()
        {
            StartDate = DateTime.Now;
            Deadline = DateTime.Now;
            EndDate = null;
            status = "в ожидании";
            
        }

        public string Title { get; set; }
        public string? Description { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public DateTime Deadline { get; set; }
        public string AssignedTo { get; set; }
        //public string Status { get; set; }

        private string status;
        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        
        

        
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
